using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class GPIO : Test
    {
        TestTool testTool;
        CS381 cs381;
        public string testName = "TEST GPIO";
        public string errorMessage = "";
        public bool result = false;

        public GPIO(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
            directLog("======================TEST GPIO SCHEDA ESPANSIONE=======================", 2);
            result = true;
        }

        public override void runTest()
        {

            

            directLog("GPIO0 -> BASSO | GPIO1 -> BASSO",0);
            cs381.setGPIO(Modbus.C_ANA_GP0, false);
            cs381.setGPIO(Modbus.C_ANA_GP1, false);
                    

            Thread.Sleep(100);

            var digitalOutputs = testTool.getDigitalOutputs();
            if (digitalOutputs["GPIO0"] == true && digitalOutputs["GPIO1"] == true)
            {
                directLog(" -> OK", 1);
            }
            else
            {
                directLog(" -> FALLITO", 1);
                result = false;
            }


            directLog("GPIO0 -> ALTO | GPIO1 -> BASSO", 0);
            cs381.setGPIO(Modbus.C_ANA_GP0, true);
            cs381.setGPIO(Modbus.C_ANA_GP1, false);

            Thread.Sleep(100);

            digitalOutputs = testTool.getDigitalOutputs();

            if (digitalOutputs["GPIO0"] == false && digitalOutputs["GPIO1"] == true)
            {
                directLog(" -> OK", 1);
            }
            else
            {
                directLog(" -> FALLITO", 1);
                result = false;
            }


            directLog("GPIO0 -> BASSO | GPIO1 -> ALTO", 0);
            cs381.setGPIO(Modbus.C_ANA_GP0, false);
            cs381.setGPIO(Modbus.C_ANA_GP1, true);

            Thread.Sleep(100);

            digitalOutputs = testTool.getDigitalOutputs();

            if (digitalOutputs["GPIO0"] == true && digitalOutputs["GPIO1"] == false)
            {
                directLog(" -> OK", 1);
            }
            else
            {
                directLog(" -> FALLITO", 1);
                result = false;
            }

            directLog("GPIO0 -> ALTO | GPIO1 -> ALTO", 0);
            cs381.setGPIO(Modbus.C_ANA_GP0, true);
            cs381.setGPIO(Modbus.C_ANA_GP1, true);

            Thread.Sleep(100);

            digitalOutputs = testTool.getDigitalOutputs();

            if (digitalOutputs["GPIO0"] == false && digitalOutputs["GPIO1"] == false)
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
