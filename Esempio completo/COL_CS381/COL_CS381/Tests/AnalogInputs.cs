using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class AnalogInputs : Test
    {
        TestTool testTool;
        CS381 cs381;
        public string testName = "TEST INGRESSI ANALOGICI";
        public string errorMessage = "VERIFICARE DI AVER CONNESSO GLI INGRESSI PH E RX \r\n" + "FALLITO, VERIFICARE DI AVER CONNESSO GLI INGRESSI PH E RX \r\n";
        public bool result = false;

        public AnalogInputs(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
            directLog("===================TEST INGRESSI ANALOGICI=====================", 2);
            result = true; 
        }

        public override void tearDown()
        {

            testTool.send(TestTool.ALL_LOW);


            if (result) testTool.beep_seq();
            else testTool.beep_long();

            directLog("", 2);


            directLog(TestTool.assertTrue(result), 2);

            directLog("_____________________________________________________", 2);

           
        }

        public override void runTest()
        {
            testTool.send(TestTool.SET_REFERENCE_LOW);

            Thread.Sleep(1000);

            Dictionary<string, int> analogInputs = cs381.getAnalogInputs();

            directLog("TEST TUTTI INGRESSI BASSI", 2);

            checkAndLog(analogInputs.ElementAt(0), TestTool.PH_REFERENCE_LOW, TestTool.PH_TOLERANCE);
            checkAndLog(analogInputs.ElementAt(1), TestTool.RX_REFERENCE_LOW, TestTool.RX_TOLERANCE);
            checkAndLog(analogInputs.ElementAt(2), TestTool.TEMP_REFERENCE, TestTool.TEMP_TOLERANCE);
            checkAndLog(analogInputs.ElementAt(3), TestTool.I_IN_4_20mA_REFERENCE_LOW, TestTool.I_IN_4_20mA_TOLERANCE);

            testTool.send(TestTool.SET_REFERENCE_HIGH);

            Thread.Sleep(3000);

            analogInputs = cs381.getAnalogInputs();
            directLog("", 1);
            directLog("TEST TUTTI INGRESSI ALTI", 2);

            checkAndLog(analogInputs.ElementAt(0), TestTool.PH_REFERENCE_HIGH, TestTool.PH_TOLERANCE);
            checkAndLog(analogInputs.ElementAt(1), TestTool.RX_REFERENCE_HIGH, TestTool.RX_TOLERANCE);
            checkAndLog(analogInputs.ElementAt(2), TestTool.TEMP_REFERENCE, TestTool.TEMP_TOLERANCE);
            checkAndLog(analogInputs.ElementAt(3), TestTool.I_IN_4_20mA_REFERENCE_HIGH, TestTool.I_IN_4_20mA_TOLERANCE);

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

        private void checkAndLog(KeyValuePair<string, int> value, int target, int tolerance)
        {
            bool tempResult = true;
            if (!TestTool.checkResult(value.Value, target, tolerance))
            {
                result = false;
                tempResult = false;
            }

            directLog(TestTool.formatResult(value.Key, target,tolerance, value.Value, tempResult), 1);
        }
    }
}
