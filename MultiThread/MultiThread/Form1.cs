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
        Thread thread1;
        Thread thread2;

        public delegate void UpdateLog(string text);
        public UpdateLog delegatoLog;

        public Form1()
        {

            InitializeComponent();
            this.delegatoLog = new UpdateLog(addToLog);

            tClass1 = new Tests.ThreadClass("thread_1");
            tClass2 = new Tests.ThreadClass("thread_2");
            tClass1.log += new Tests.ThreadClass.updateText(log);
            tClass2.log += new Tests.ThreadClass.updateText(log);

            thread1 = new Thread(tClass1.DoWork);
            thread2 = new Thread(tClass2.DoWork);

        }

        private void log(string text)
        {
           //delegatoLog(text);
            Invoke(delegatoLog, new object[] { text });
        }

        public void addToLog(string text)
        {
            textBox1.Text += text + "\r\n";
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            thread1.Start();
            thread2.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread1.Abort();
            thread2.Abort();
        }
    }
}
