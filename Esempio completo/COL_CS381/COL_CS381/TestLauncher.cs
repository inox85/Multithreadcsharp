using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EasyModbus;

namespace COL_CS381
{
    class TestLauncher
    {
        CS381 board;
        public delegate void updateLog(string text, int newLines);
        public event updateLog log;

        public delegate void FinalizeTests();
        public event FinalizeTests finalize;

        List<Test> tests;

        public TestLauncher(List<Test> _tests)
        {
            this.tests = _tests;
        }

        public void testThread()
        {
            try
            {
                foreach (Test t in tests)
                {
                    Test test = t;

                    test.setup();
                    test.runTest();
                    test.tearDown();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Problemi di comunicazione, assicurarsi che le porte selezionate siano corrette e che il tool di collaudo sia alimentato");
            }
            finalize();

           
        }
    }
}
