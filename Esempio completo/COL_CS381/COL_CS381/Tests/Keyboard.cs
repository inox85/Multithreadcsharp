using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class Keyboard : Test
    {
        TestTool testTool;
        CS381 cs381;
        public bool result = false;
        public string testName = "TEST TASTIERA";
        public string errorMessage = "";
        bool suPressed = false;
        bool uscitaPressed = false;
        bool invioPressed = false;
        bool giuPressed = false;

        public Keyboard(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
            directLog("======================TEST TASTIERA=======================", 2);
            result = true;
        }

        public override void runTest()
        {
            directLog("SIMULAZIONE TASTIERA AVVIATA", 2);

            testTool.keyboardTest();

            for (int i = 0; i < 30; i++)
            {
                var keys = cs381.getKeyboard();

                if (keys.ElementAt(0).Value && !suPressed)
                {
                    suPressed = true;
                    directLog("SU PREMUTO", 1);
   
                }

                if (keys.ElementAt(1).Value && !invioPressed)
                {
                    invioPressed = true;
                    directLog("INVIO PREMUTO", 1);

                }

                if (keys.ElementAt(2).Value && !uscitaPressed)
                {
                    uscitaPressed = true;
                    directLog("USCITA PREMUTO", 1);
                    testTool.beep();                }

                if (keys.ElementAt(3).Value && !giuPressed)
                {
                    giuPressed = true;
                    directLog("GIU PREMUTO", 1);
                    testTool.beep();                }
                result = false;

                if (suPressed && invioPressed && uscitaPressed && giuPressed)
                {
                    directLog("", 1);
                    directLog("LA TASTIERA E' STATA VERIFICATA CORRETTAMENTE CORRETTAMENTE", 1);
                    result = true;
                    break;
                }

                
            }
        }

        public override void tearDown()
        {
            if(result == false) directLog("TIMEOUT", 1);


            if (result) testTool.beep_seq();
            else testTool.beep_long();

            directLog("", 2);

            if (result) testTool.beep_seq();
            else testTool.beep_long();

            directLog(TestTool.assertTrue(result), 2);

            directLog("_____________________________________________________", 2);

            

            Thread.Sleep(1000);
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
