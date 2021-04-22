using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EasyModbus;
using System.IO.Compression;
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace COL_CS381
{
    public partial class frmMain : Form
    {
        Thread tests;
        TestTool testTool;
        CS381 cs381;
        IniParser config;

        public delegate void UpdateLog(string text);
        public UpdateLog log;

        public delegate void UpdateLabel(string text, bool result);
        public UpdateLabel updateLabel;

        public delegate void FinalizeTests(string text);
        public FinalizeTests finalize;

        List<Test> testList;
        DateTime start;

        public frmMain()
        {
            InitializeComponent();   
        }

        private void btnAvviaCollaudo_Click(object sender, EventArgs e)
        {
            if(tbOperatore.Text == "")
            {
                MessageBox.Show("Indicare il nome dell'operatore");
                return;
            }

            start = DateTime.Now;

            tbOutput.Text = "";
            lblResult.Visible = false;
            lblNumberReport.Visible = false;


            if(toolSerial.IsOpen) toolSerial.Close();
            toolSerial.PortName = cbToolPort.Text;

            testTool = new TestTool(toolSerial);

            if(cs381 == null) cs381 = new CS381(boardSerial);

            cellSerial.Close();
            cellSerial.PortName = cbCellPort.Text;           


            logOutput("********************************************************************************************");
            logOutput("OPERATORE: " + tbOperatore.Text.ToUpper());
            logOutput("DATA/ORA: " + DateTime.Now.ToString());
            logOutput("********************************************************************************************");

            testList = new List<Test>();

            testList.Add(new Tests.Keyboard(testTool, cs381));
            testList.Add(new Tests.Voltages(testTool, cs381));
            testList.Add(new Tests.GPIO(testTool, cs381));
            testList.Add(new Tests.DigIn(testTool, cs381));
            testList.Add(new Tests.DigOut(testTool, cs381));
            testList.Add(new Tests.Motors(testTool, cs381));
            testList.Add(new Tests.AnalogInputs(testTool, cs381));
            testList.Add(new Tests.OutputCurrents(testTool, cs381));
            testList.Add(new Tests.Flash_EEPROM(testTool, cs381));
            testList.Add(new Tests.Rs485(testTool, cs381, cellSerial));


            for (int i = 0; i < testList.Count; i++)
            {
                testList[i].log += new Test.updateLog(logOutput);
            }

            TestLauncher testLauncher = new TestLauncher(testList);

            testLauncher.log += new TestLauncher.updateLog(logOutput);
            testLauncher.finalize += new TestLauncher.FinalizeTests(finalizeTests);
            

            this.log = new UpdateLog(addToLog);
            this.updateLabel= new UpdateLabel(updateLabelResult);

            tests = new Thread(() => testLauncher.testThread());
            tests.Start();

            btnAvviaCollaudo.Enabled = false;


        }


        private void logOutput(string description)
        {
            string textToAdd = "";

            textToAdd += description + "\r\n";
            tbOutput.Text += textToAdd;
            this.Refresh();
        }

        private void updateLabelResult(string path, bool result)
        {
            if (result)
            {
                lblResult.Visible = true;
                lblResult.ForeColor = Color.DarkGreen;
                lblResult.Text = "TEST SUPERATI - Report generato in: " + path;
                lblNumberReport.Visible = true;
                lblNumberReport.ForeColor = Color.DarkGreen;
                lblNumberReport.Text = "Report associato alla scheda: " + TextFile.getLastReportNumber(config.GetSetting("CONFIG", "REPORT")).ToString();
            }
            else
            {
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "TEST FALLITI - NESSUN REPORT GENERATO";
            }

            TimeSpan timeItTook = DateTime.Now - start;

            logOutput("********************************************************************************************");
            logOutput("Tempo di esecuzione: " + (timeItTook.Minutes * 60 + timeItTook.Seconds).ToString() + " secondi" );
            logOutput("********************************************************************************************");

            testTool.beep();
            Thread.Sleep(100);
            testTool.beep();
            Thread.Sleep(100);
            testTool.beep();
            Thread.Sleep(100);
            testTool.beep();
            Thread.Sleep(100);
            testTool.beep();
            Thread.Sleep(100);
            testTool.beep();
            Thread.Sleep(100);
            btnAvviaCollaudo.Enabled = true;
        }

        private void finalizeTests()
        {
            bool allTestsOk = true;

            Invoke(log, new object[] { "============= RESOCONTO DEI TEST ==============\r\n\r\n" });

            foreach (Test test in testList)
            {
                Invoke(log, new object[] { test.getTestName() });

                if (test.getResult() == true)
                {
                    Invoke(log, new object[] { " -> OK\r\n\r\n" });
                }
                else
                {
                    MessageBox.Show("Attenzione: " + test.getTestName() + " FALLITO!" );
                    Invoke(log, new object[] { " -> ## FALLITO ##\r\n\r\n" });
                    allTestsOk = false;
                }
            }

            if (allTestsOk)
            {
                string reportPath = TextFile.writeReportOnServer(tbOutput.Text, config.GetSetting("CONFIG", "REPORT"));
                Invoke(log, new object[] { "Report generato in: " + reportPath + "\r\n\r\n" });
                Invoke(updateLabel, new object[] { "Report generato in: " + reportPath , allTestsOk}); ;
            }
            else
            {
                Invoke(log, new object[] { "NON TUTTI I TEST SONO STATI SUPERATI CON SUCCESSO, NON VERRA' GENERATO NESSUN REPORT ASSOCIATO ALLA SCHEDA\r\n\r\n" });
                Invoke(updateLabel, new object[] { "", allTestsOk }); ;
                TextFile.writeReportFailed(config.GetSetting("CONFIG", "REPORT_FALLITI"), tbOutput.Text);
            }

        }

        private void logOutput(string text, int newLines)
        {
            string nl = "";

            for(int i = 0; i < newLines; i++)
            {
                nl += "\r\n";
            }

            Invoke(log, new object[] { text + nl });
            
        }

        private void addToLog(string text)
        {
            tbOutput.Text += text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testTool = new TestTool(toolSerial);

            testTool.getDigitalOutputs();
        }

        private void tbOutput_TextChanged(object sender, EventArgs e)
        {
            tbOutput.SelectionStart = tbOutput.TextLength;
            tbOutput.ScrollToCaret();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           config = new IniParser(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
           cbBoardPort.DataSource = SerialPort.GetPortNames();
           cbToolPort.DataSource = SerialPort.GetPortNames();
           cbCellPort.DataSource = SerialPort.GetPortNames();

           if(cbToolPort.Items.Count > 0)
           {
                cbBoardPort.SelectedIndex = 0;
                cbToolPort.SelectedIndex = 0;
                cbCellPort.SelectedIndex = 0;
           }

        }

        private void cbToolPort_Click(object sender, EventArgs e)
        {
            
          cbToolPort.DataSource = SerialPort.GetPortNames();
   

        }

        private void cbBoardPort_Click(object sender, EventArgs e)
        {
            cbBoardPort.DataSource = SerialPort.GetPortNames();
        }

        private void cbCellPort_Click(object sender, EventArgs e)
        {
            cbCellPort.DataSource = SerialPort.GetPortNames();
             
        }



        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rilevaPorteAutomarticamenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ports = SerialPort.GetPortNames();

            bool toolPort = false;
            bool boardPort = false;
            bool cellPort = false;

            foreach(string port  in ports)
            {
                SerialPort sp = new SerialPort(port);
                try
                {
                    sp.BaudRate = 9600;
                    sp.Parity = Parity.None;
                    sp.StopBits = StopBits.One;
                    sp.DataBits = 8;
                    sp.Close();
                    if (!sp.IsOpen) sp.Open();
                    sp.Write("3");
                    Thread.Sleep(100);
                    string value = sp.ReadExisting();
                    if (value.Contains("3"))
                    {
                        cbToolPort.Text = port;
                        toolPort = true;
                        sp.Close();
                        sp.Dispose();
                        break;
                    }
                    sp.Close();
                    sp.Dispose();
                }
                catch (Exception)
                {
                    sp.Close();
                    sp.Dispose();
                }
            }

            foreach (string port in ports)
            {
                if (port != cbToolPort.Text)
                {
                    ModbusClient board = new ModbusClient();
                    board.SerialPort = port;
                    board.UnitIdentifier = 1; //Not necessary since default slaveID = 1;
                    board.Baudrate = 9600;

                    board.Parity = System.IO.Ports.Parity.None;
                    board.StopBits = System.IO.Ports.StopBits.One;
                    try
                    {
                        board.Connect();
                        bool value = board.ReadDiscreteInputs(7, 1)[0];
                        cbBoardPort.Text = port;
                        boardPort = true;
                        foreach (string p in ports)
                        {
                            if (p != cbToolPort.Text && p != cbBoardPort.Text)
                            {
                                SerialPort cPort = new SerialPort(p);
                                cPort.BaudRate = 9600;
                                cPort.Parity = Parity.None;
                                cPort.StopBits = StopBits.One;
                                cPort.DataBits = 8;

                                try
                                {
                                    cPort.Open();
                                    board.WriteMultipleRegisters(Modbus.MR_TEST_SERIAL_PORT, new int[] { Modbus.EXECUTE });
                                    Thread.Sleep(100);
                                    
                                    var v = cPort.ReadExisting();
                                    if (v.Contains("HELLO"))
                                    {
                                        cbCellPort.Text = p;
                                        cellPort = true;
                                        cPort.Close();
                                        cPort.Dispose();
                                        break;
                                    }
                                    cPort.Close();
                                    cPort.Dispose();
                                }
                                catch(Exception ex)
                                {

                                    int a = 10;
                                    
                                }
                            }
                        }

                        board.Disconnect();
                        SerialPort sp = new SerialPort(port);
                        sp.Close();
                        sp.Dispose();
                        break;
                    }
                    catch(Exception ex)
                    {
                        board.Disconnect();
                        SerialPort sp = new SerialPort(port);
                        sp.Close();
                        sp.Dispose();
                    }
                }
            }

        }

        private void apriCartellaReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(config.GetSetting("CONFIG", "REPORT")))
                Process.Start(config.GetSetting("CONFIG", "REPORT"));
            else MessageBox.Show("LA directory dei report non esiste");
        }

      

        private void inviaPacchettoReportAChemitecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "temp"))) 
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "temp"));
            }

            string code = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string file = Path.Combine(Directory.GetCurrentDirectory(), "temp", "REPORTS_" + code + ".zip");

           

            using (ZipFile zip = new ZipFile())
            {
                // add this map file into the "images" directory in the zip archive
                zip.AddDirectory(config.GetSetting("CONFIG", "REPORT"));
                zip.Save(file);
            }

            
            var smtpClient = new SmtpClient("mail.chemitec.it")
            {
                Port = 25,
                Credentials = new NetworkCredential("d.innocenti@chemitec.it", "@Verde6100"),
                EnableSsl = false,
            };



            try
            {
                string to = "d.innocenti@chemitec.it";
                string from = "d.innocenti@chemitec.it";
                MailMessage message = new MailMessage(from, to);
                //message.CC.Add(new MailAddress("f.bausi@chemitec.it"));
                message.CC.Add(new MailAddress("d.innocenti@chemitec.it"));
                //message.CC.Add(new MailAddress("s.saporiti@chemitec.it"));
                message.CC.Add(new MailAddress("f.catelani@chemitec.it"));
                message.CC.Add(new MailAddress("n.bellucci@chemitec.it"));
                //message.CC.Add(new MailAddress("j.mataich@chemitec.it"));
                //message.CC.Add(new MailAddress("h.gllasoviku @chemitec.it"));
                // Create  the file attachment for this email message.
                Attachment data = new Attachment(file, MediaTypeNames.Application.Zip);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                // Add the file attachment to this email message.
                message.Attachments.Add(data);

                message.Subject = "Report collaudo 4001 - EUREL " + code ;
                MailAddressCollection destinatari = new MailAddressCollection();
                message.Body = "Invio report di collaudo EUREL generato automaticamente";

                //smtpClient.Send("d.innocenti@chemitec.it", "m.pasquariello@chemitec.it", "Ordine PROGA", tbStringaRapida.Text);
                //smtpClient.Send("d.innocenti@chemitec.it", "f.bausi@chemitec.it", "Ordine PROGA", tbStringaRapida.Text);
                //smtpClient.Send("d.innocenti@chemitec.it", "d.innocenti@chemitec.it", "Ordine PROGA", tbStringaRapida.Text);
                //smtpClient.Send("d.innocenti@chemitec.it", "s.saporiti@chemitec.it", "Ordine PROGA", tbStringaRapida.Text);
                //smtpClient.Send("d.innocenti@chemitec.it", "f.catelani@chemitec.it", "Ordine PROGA", tbStringaRapida.Text);
                //smtpClient.Send("d.innocenti@chemitec.it", "n.bellucci@chemitec.it", "Ordine PROGA", tbStringaRapida.Text);

                smtpClient.Send(message);

                MessageBox.Show("Mail correttamente inviata");

            }
            catch(Exception ex)
            {
                MessageBox.Show("Errore nell'invio della mail:\r\n\r\n" + ex);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "temp")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "temp"));
            }

            string code = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string file = Path.Combine(Directory.GetCurrentDirectory(), "temp", "REPORTS_" + code + ".zip");



            using (ZipFile zip = new ZipFile())
            {
                // add this map file into the "images" directory in the zip archive
                zip.AddDirectory(config.GetSetting("CONFIG", "REPORT"));
                zip.Save(file);
            }


            var smtpClient = new SmtpClient("mail.chemitec.it")
            {
                Port = 25,
                Credentials = new NetworkCredential("d.innocenti@chemitec.it", "@Verde6100"),
                EnableSsl = false,
            };



            try
            {
                string to = "d.innocenti@chemitec.it";
                string from = "d.innocenti@chemitec.it";
                MailMessage message = new MailMessage(from, to);
                message.CC.Add(new MailAddress("d.innocenti@chemitec.it"));
                // Create  the file attachment for this email message.
                Attachment data = new Attachment(file, MediaTypeNames.Application.Zip);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                // Add the file attachment to this email message.
                message.Attachments.Add(data);

                message.Subject = "Report collaudo 4001 - EUREL " + code;
                MailAddressCollection destinatari = new MailAddressCollection();
                message.Body = "Invio report di collaudo EUREL generato automaticamente";

                smtpClient.Send(message);

            }
            catch
            {

            }
        }
    }
}
