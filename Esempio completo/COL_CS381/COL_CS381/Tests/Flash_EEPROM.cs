using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class Flash_EEPROM : Test
    {
        TestTool testTool;
        CS381 cs381;
        public string testName = "TEST FLASH E EEPROM";
        public string errorMessage = "";
        public bool result = false;

        public Flash_EEPROM(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
            directLog("======================TEST FLASH - EEPROM=======================", 2);
            result = true;
        }

        public override void runTest()
        {
            directLog("ESEGUO TEST FLASH", 0);
            cs381.startTest(Modbus.MR_TEST_FLASH, 1);
            Thread.Sleep(100);
            if(cs381.getTestResult(Modbus.MR_TEST_FLASH) == 0)
            {
                directLog(" -> OK", 1);               
            }
            else
            {
                directLog(" -> FALLITO", 1);
                result = false;
            }

            directLog("ESEGUO TEST EEPROM", 0);


            cs381.startTest(Modbus.MR_EEPROM, 1);
            Thread.Sleep(100);
            if (cs381.getTestResult(Modbus.MR_EEPROM) == 0)
            {
                directLog(" -> OK", 1);
            }
            else
            {
                directLog(" -> FALLITO", 1);
                result = false;
            }





        }

        public override void tearDown()
        {

            if (result) testTool.beep_seq();
            else testTool.beep_long();

            directLog("", 2);

            directLog(TestTool.assertTrue(result), 2);

            directLog("_____________________________________________________", 2);

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
    }
}
