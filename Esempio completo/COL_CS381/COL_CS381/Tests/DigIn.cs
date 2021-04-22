using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class DigIn : Test
    {
        TestTool testTool;
        CS381 cs381;
        public string testName = "TEST INGRESSI DIGITALI";
        public string errorMessage = "";
        public bool result = false;

        bool[,] digInPattern = {  { true,  true,   true, false   },      // 0
                                  { false,  true,   true,  false  },     // 1
                                  { true,  false,  true,  false  },      // 2
                                  { true, true,  false,  false   },      // 3
                                  { false, false,  false, false   }};    // 4

        public DigIn(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
           directLog("===================TEST DIGITAL INPUT=====================", 2);
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

            testTool.send(TestTool.ALL_LOW);

            Dictionary<string, bool> digitalInputs = cs381.getDigitalInputs();

            directLog("TEST TUTTI INGRESSI BASSI", 2);

            for (int i = 0; i < digitalInputs.Count-1; i++)
            {
                if (digitalInputs.ElementAt(i).Value == digInPattern[0,i])
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> OK", 1);
                }
                else
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> FALLITO", 1);
                    result = false;
                }
            }

            testTool.send(TestTool.SET_DIGITAL_INPUT_1);

            Thread.Sleep(100);

            digitalInputs = cs381.getDigitalInputs();

            directLog("", 1);
            directLog("TEST REAG1 ALTO", 2);

            for (int i = 0; i < digitalInputs.Count - 1; i++)
            {
                if (digitalInputs.ElementAt(i).Value == digInPattern[1, i])
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> OK", 1);
                }
                else
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> FALLITO", 1);
                    result = false;
                }
            }

            testTool.send(TestTool.SET_DIGITAL_INPUT_2);

            Thread.Sleep(100);

            digitalInputs = cs381.getDigitalInputs();

            directLog("", 1);
            directLog("TEST REAG2 ALTO", 2);

            for (int i = 0; i < digitalInputs.Count - 1; i++)
            {
                if (digitalInputs.ElementAt(i).Value == digInPattern[2, i])
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> OK", 1);
                }
                else
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> FALLITO", 1);
                    result = false;
                }
            }

            testTool.send(TestTool.SET_DIGITAL_INPUT_3);

            Thread.Sleep(100);

            digitalInputs = cs381.getDigitalInputs();

            directLog("", 1);
            directLog("TEST REAG3 ALTO", 2);

            for (int i = 0; i < digitalInputs.Count - 1; i++)
            {
                if (digitalInputs.ElementAt(i).Value == digInPattern[3, i])
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> OK", 1);
                }
                else
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> FALLITO", 1);
                    result = false;
                }
            }

            testTool.send(TestTool.SET_ALL_DIGITAL_INPUTS_HIGH);

            Thread.Sleep(100);

            digitalInputs = cs381.getDigitalInputs();

            directLog("", 1);
            directLog("TEST TUTTI INGRESSI ALTI", 2);

            for (int i = 0; i < digitalInputs.Count - 1 ; i++)
            {
                if (digitalInputs.ElementAt(i).Value == digInPattern[4, i])
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> OK", 1);
                }
                else
                {
                    directLog(digitalInputs.ElementAt(i).Key + "-> FALLITO", 1);
                    result = false;
                }
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
