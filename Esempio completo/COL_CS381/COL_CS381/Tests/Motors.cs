using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class Motors : Test
    {
        TestTool testTool;
        CS381 cs381;
        public string testName = "TEST MOTORI";
        public string errorMessage = "";
        public bool result = false;

        public Motors(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
            directLog("======================TEST MOTORI PERISTALTICHE=======================", 2);
            result = true;
        }


        public override void runTest()
        {
            cs381.setMotor(Modbus.C_MOTOR0, false);
            cs381.setMotor(Modbus.C_MOTOR1, false);

            Thread.Sleep(100);

            var motors = testTool.getMotors();
            directLog("", 1);
            checkAndLog(motors.ElementAt(0), TestTool.MOTOR_ZERO, TestTool.MOTOR_TOLERANCE);
            checkAndLog(motors.ElementAt(1), TestTool.MOTOR_ZERO, TestTool.MOTOR_TOLERANCE);

            cs381.setMotor(Modbus.C_MOTOR0, true);
            cs381.setMotor(Modbus.C_MOTOR1, false);

            Thread.Sleep(100);

            motors = testTool.getMotors();

            directLog("", 1);
            checkAndLog(motors.ElementAt(0), TestTool.MOTOR_REFERENCE, TestTool.MOTOR_TOLERANCE);
            checkAndLog(motors.ElementAt(1), TestTool.MOTOR_ZERO, TestTool.MOTOR_TOLERANCE);

            cs381.setMotor(Modbus.C_MOTOR0, false);
            cs381.setMotor(Modbus.C_MOTOR1, true);

            Thread.Sleep(100);

            motors = testTool.getMotors();

            directLog("", 1);
            checkAndLog(motors.ElementAt(0), TestTool.MOTOR_ZERO, TestTool.MOTOR_TOLERANCE);
            checkAndLog(motors.ElementAt(1), TestTool.MOTOR_REFERENCE, TestTool.MOTOR_TOLERANCE);

            cs381.setMotor(Modbus.C_MOTOR0, true);
            cs381.setMotor(Modbus.C_MOTOR1, true);

            Thread.Sleep(100);

            motors = testTool.getMotors();

            directLog("", 1);
            checkAndLog(motors.ElementAt(0), TestTool.MOTOR_REFERENCE, TestTool.MOTOR_TOLERANCE);
            checkAndLog(motors.ElementAt(1), TestTool.MOTOR_REFERENCE, TestTool.MOTOR_TOLERANCE);


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
