using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiThread
{
    public partial class Form1 : Form
    {
        Tests.ThreadClass tClass1;
        Tests.ThreadClass tClass2;
        Tests.ThreadClass serialManager;
        Thread thread1;
        Thread thread2;
        Thread sThread;

        public delegate void ReceiveDelegate(string StringaRicevuta);
        public ReceiveDelegate updateLog;

        public Form1()
        {

            InitializeComponent();
            
            serialManager = new Tests.ThreadClass("", serialPort);
            serialManager.log += new Tests.ThreadClass.updateLog(log);

            this.updateLog = new ReceiveDelegate(writeOutput);


        }

        private void log(string indata)
        {
            Invoke(updateLog, new object[] { indata });

        }

        public void writeOutput(string text)
        {
            textBox1.Text += text + "\r\n";
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialManager = new Tests.ThreadClass("b", serialPort);
            sThread = new Thread(serialManager.serialWrite);
            sThread.Start();
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sThread.Abort();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sThread = new Thread(() => serialManager.serialWrite("b"));
            sThread.Start();
        }
    }
}
