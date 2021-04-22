using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class Voltages : Test
    {
        TestTool testTool;
        CS381 cs381;
        public string testName = "TEST TENSIONI A BORDO SCHEDA";
        public string errorMessage = "";
        public bool result = false;

        public Voltages(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
            directLog("======================TEST TENSIONI A BORDO SCHEDA=======================", 2);
            result = true;
        }

        public override void runTest()
        {
            var voltages = testTool.getVoltages();

            cs381.setVdcPT500(true);

            Thread.Sleep(100);

            directLog("TEST TENSIONI DI ALIMENTAZIONEE TENSIONE PT500", 2);

            checkAndLog(voltages.ElementAt(0), TestTool.MAIN_24VDC_REFERENCE, TestTool.MAIN_24VDC_TOLERANCE);
            checkAndLog(voltages.ElementAt(1), TestTool.MAIN_5VDC_REFERENCE, TestTool.MAIN_5VDC_TOLERANCE);
            checkAndLog(voltages.ElementAt(2), TestTool.MAIN_5VDC_BKP_REFERENCE, TestTool.MAIN_5VDC_BKP_TOLERANCE);
            checkAndLog(voltages.ElementAt(3), TestTool.MAIN_5VDC_REFERENCE, TestTool.MAIN_5VDC_TOLERANCE);
            checkAndLog(voltages.ElementAt(4), TestTool.MAIN_24VDC_REFERENCE, TestTool.MAIN_24VDC_TOLERANCE);
        }

        public override void tearDown()
        {



            if (result) testTool.beep_seq();
            else testTool.beep_long();

            directLog("", 2);

            directLog(TestTool.assertTrue(result), 2);

            directLog("_____________________________________________________", 2);

        }

         



        private void checkAndLog(KeyValuePair<string, float> value, float target, float tolerance)
        {
            bool tempResult = true;
            if (!TestTool.checkResult(value.Value, target, tolerance))
            {
                result = false;
                tempResult = false;
            }

            directLog(TestTool.formatResult(value.Key, target, tolerance, value.Value, tempResult), 1);
        }

        public override string getErrorMessage()
        {
            return errorMessage;
        }

        public override bool getResult()
        {
            return result;
        }

        public override string getTestName()
        {
            return testName;
        }
    }
}
