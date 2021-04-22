using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class DigOut : Test
    {
        TestTool testTool;
        CS381 cs381;
        public string testName = "TEST USCITE DIGITALI";
        public string errorMessage = "";
        public bool result = false;

        bool[,] digOutState = { { false, false, false, false, false, false, false },
                                { true, false, false, false, false, false, false  },
                                { false, true, false, false, false, false, false  },
                                { false, false, true, false, false, false, false  },
                                { false, false, false, true, false, false, false  },
                                { false, false, false, false, true, false, false  },
                                { false, false, false, false, false, true, false  },
                                { false, false, false, false, false, false, true  },
                                { true,  true,  true,  true,  true,  true,  true  }};
          
        bool[,] digOutPattern = { { false, false, false, false, false, false, false , true },    // 0
                                  { true, false, false, false, false, false, false , true  },    // 1
                                  { false, true, false, false, false, false, false , true },    // 2
                                  { false, false, true, false, false, false, false , true },    // 3
                                  { false, false, false, true, false, false, false , true },    // 4
                                  { false, false, false, false, false, true, false , true },    // 5
                                  { false, false, false, false, false, false, true , true },    // 6
                                  { false, false, false, false, true, false,  false , false  },    // 7
                                  { true,  true,  true,  true,  true,  true,  true , false  }};   // 8


        public DigOut(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
            directLog("===================TEST DIGITAL OUTPUT=====================", 2);
            result = true;
        }

        public override void tearDown()
        {

            if (result) testTool.beep_seq();
            else testTool.beep_long();


            directLog("", 2);

            directLog(TestTool.assertTrue(result), 2);

            directLog("_____________________________________________________", 2);
        }

        public override void runTest()
        {
            
            Dictionary<string, int> digitalOutputsRegiser = new Dictionary<string, int>();
            digitalOutputsRegiser.Add("RELAY1", Modbus.C_RELAY1_STATUS);
            digitalOutputsRegiser.Add("RELAY2", Modbus.C_RELAY2_STATUS);
            digitalOutputsRegiser.Add("RELAY3", Modbus.C_RELAY3_STATUS);
            digitalOutputsRegiser.Add("RELAY4", Modbus.C_RELAY4_STATUS);
            digitalOutputsRegiser.Add("RELAY_ALLARME", Modbus.C_RELAY_ALARM_STATUS);
            digitalOutputsRegiser.Add("RELAY_AUX1", Modbus.C_RELAY_AUX1_STATUS);
            digitalOutputsRegiser.Add("RELAY_AUX2", Modbus.C_RELAY_AUX2_STATUS);

            var values = testTool.getDigitalOutputs();

            for (int i = 0; i< 8; i++) directLog(values.ElementAt(i).Key + " |", 0);

            directLog("", 2);

            for (int k = 0; k < 9; k++)
            {
                for (int c = 0; c < 7; c++) cs381.setDigitalOutput(Modbus.C_RELAY1_STATUS + c, digOutState[k, c]);
                //Thread.Sleep(100);

                values = testTool.getDigitalOutputs();             

                for(int i = 0; i < 8; i++)
                {
                    bool readValue = values.ElementAt(i).Value;
                    bool attendValue = digOutPattern[k, i];

                    if (attendValue == readValue)
                    {
                        directLog(" OK |", 0);                     
                    }
                    else
                    {
                        directLog("  --   |", 0);
                        result = false;
                    }

                }

                directLog("", 1);
            }


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
