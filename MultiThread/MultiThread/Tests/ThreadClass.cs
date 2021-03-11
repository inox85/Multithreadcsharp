using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread.Tests
{

    class ThreadClass
    {
        public delegate void updateText(string text);
        public event updateText log;
        private string text = "";

        public ThreadClass(string _text)
        {
            this.text = _text;
        }

        public void DoWork()
        {
            for (int i = 0; i < 10; i++)
            {

                //Console.WriteLine("Working thread...");
                log(text);
                Thread.Sleep(1000);
            }
        }
    }
}
