using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread.Tests
{

    class ThreadClass
    {
        public delegate void updateLog(string text);
        public event updateLog log;
        private string text = "";
        private SerialPort sp;


        public ThreadClass(string _text, SerialPort _sp)
        {
            
            this.sp = _sp;
            if(!sp.IsOpen) sp.Open();
            sp.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            this.text = _text;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sPort = (SerialPort)sender;
            Thread.Sleep(100);
            string data = sPort.ReadExisting();
            try
            {
                if(data != "")
                log(data);
            }
            catch
            {

            }
            int a = 10;
        }


        public void serialWrite()
        {
            if (!sp.IsOpen) sp.Open();
            sp.Write(text);
        }


        public void serialWrite(string s)
        {
            for (int i = 0; i < 10; i++)
            {
                sp.Write(s);
                Thread.Sleep(1000);
            }

        }
    }
}
