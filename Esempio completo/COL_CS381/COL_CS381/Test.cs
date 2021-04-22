using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace COL_CS381
{
    class Test
    {

        public delegate void updateLog(string text, int newLines);
        public event updateLog log;
        TestTool testTool;
        CS381 cs381;
        public string report = "";
        public string testName = "";
        public string erroMessage = "";
        public bool result = false;

        public Test(TestTool _testTool, CS381 _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public virtual void setup()
        {

        }

        public virtual void tearDown()
        {

        }

        public virtual void runTest()
        {

        }

        public virtual string getErrorMessage()
        {
            MessageBox.Show("Funzione non implementata nella classe figlio");
            return erroMessage;
        }

        public virtual string getTestName()
        {
            MessageBox.Show("Funzione non implementata nella classe figlio");
            return testName;
        }


        public virtual bool getResult()
        {
            return result;
        }

        public string addText(string text, int newLines)
        {
            string nl = "";

            for (int i = 0; i < newLines; i++)
            {
                nl += "\r\n";
            }

            return text + nl;
        }

        public void directLog(string text, int newLines)
        {
            log(text, newLines);
        }

    }
}
