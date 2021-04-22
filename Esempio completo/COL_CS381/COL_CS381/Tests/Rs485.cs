using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class Rs485 : Test
    {
        TestTool testTool;
        CS381 cs381;
        SerialPort cellSerial;
        public string testName = "TEST RS485";
        public string errorMessage = "";
        public bool result = true;

        public Rs485(TestTool _testTool, CS381 _board, SerialPort _cellSerial) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
            this.cellSerial = _cellSerial;
        }

        public override void setup()
        {
            directLog("======================TEST SERIALE RS485 CELLA=======================", 2);
            result = false;
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
            cellSerial.Open();

            Thread.Sleep(100);

            cs381.testCellSerialPort();

            Thread.Sleep(100);

            string value = cellSerial.ReadExisting();

            directLog("", 1);
            directLog("STRINGA RICEVUTA: " + value, 1);

            if (value.Contains("HELLO"))
            {
                result = true;
                directLog("RICEVUTA STRINGA CORRETTA", 1);
            }
            else
            {
                result = false;
                directLog("RICEVUTA STRINGA ERRATA ", 1);
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
