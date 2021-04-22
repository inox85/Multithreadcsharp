using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace COL_CS381.Tests
{
    class OutputCurrents : Test
    {
        TestTool testTool;
        CS381 cs381;
        public string testName = "TEST E CALIBRAZIONI CORRENTI D'USCITA";
        public string errorMessage = "";
        public bool result = false;
        Pid pidCh1;
        Pid pidCh2;
        Pid pidCh3;
        Pid pidCh4;

        int digitCh1 = 365;
        int digitCh2 = 365;
        int digitCh3 = 365;

        float kpl = 100F;
        float kil = 1F;


        float kph = 50F;
        float kih = 1F;


        public OutputCurrents(TestTool _testTool, CS381 _board) : base(_testTool, _board)
        {
            this.testTool = _testTool;
            this.cs381 = _board;
        }

        public override void setup()
        {
            directLog("===================CALIBRAZIONE CORRENTI D'USCITA=====================", 2);
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
            directLog("CALIBRAZIONE a 0mA", 2);

            

            cs381.setCurrentCannelsDigit(digitCh1, digitCh2, digitCh3);

            Thread.Sleep(100);

            var currents = testTool.getCurrents();

            pidCh1 = new Pid(kpl, kil, TestTool.OUTPUT_CURRENT_REFERENCE_LOW);
            pidCh2 = new Pid(kpl, kil, TestTool.OUTPUT_CURRENT_REFERENCE_LOW);
            pidCh3 = new Pid(kpl, kil, TestTool.OUTPUT_CURRENT_REFERENCE_LOW);

            result = false;

            for (int i = 0; i < 100; i++)
            {

                if (i >= 99)
                {
                    directLog("################# TEST FALLITO PER TROPPE ITERAZIONI ################", 1);
                    result = false;
                    break;
                }

                float current1 = currents["I_CL2"];
                float current2 = currents["I_PH"];
                float current3 = currents["I_AUX"];


                int pidCh1out = (int)pidCh1.run(currents["I_CL2"]);
                int pidCh2out = (int)pidCh2.run(currents["I_PH"]);
                int pidCh3out = (int)pidCh3.run(currents["I_AUX"]);


                digitCh1 += pidCh1out;
                digitCh2 += pidCh2out;
                digitCh3 += pidCh3out;

                formatAndWriteOutput("I_CL2", current1, digitCh1, pidCh1out, 0);
                formatAndWriteOutput("I_PH", current2, digitCh2, pidCh2out, 0);
                formatAndWriteOutput("I_AUX", current3, digitCh3, pidCh3out, 1);
                cs381.setCurrentCannelsDigit(digitCh1, digitCh2, digitCh3);
                currents = testTool.getCurrents();

                if (chkCurrents(0.02F, current2, current3, TestTool.OUTPUT_CURRENT_REFERENCE_LOW, TestTool.OUTPUT_CURRENT_TOLERANCE_LOW))
                {
                    directLog("", 1);
                    directLog("Corrente I_CL2 in target -> " + current1.ToString() + " mA -> [" + digitCh1.ToString() + "]"   , 1) ;
                    directLog("Corrente I_PH in target -> " + current2.ToString() + " mA -> " + digitCh2.ToString() + " LSB", 1);
                    directLog("Corrente I_AUX in target -> " + current3.ToString() + " mA -> " + digitCh3.ToString() + " LSB", 2);

                    directLog("VERIFICO TOLLERANZE DIGIT PUNTO BASSO", 0);

                    if (TestTool.checkResult(digitCh1, TestTool.OUTPUT_CURRENT_DIGIT_LOW_REFERENCE, TestTool.OUTPUT_CURRENT_DIGIT_LOW_TOLERANCE) && TestTool.checkResult(digitCh2, TestTool.OUTPUT_CURRENT_DIGIT_LOW_REFERENCE, TestTool.OUTPUT_CURRENT_DIGIT_LOW_TOLERANCE) && TestTool.checkResult(digitCh3, TestTool.OUTPUT_CURRENT_DIGIT_LOW_REFERENCE, TestTool.OUTPUT_CURRENT_DIGIT_LOW_TOLERANCE))
                    {
                        directLog("-> OK", 1);
                        directLog("SCRIVO VALORI PUNTO ALTO SU REGISTRI DI CALIBRAZIONE", 1);
                        cs381.setOutputsCurrentsCalDigitHigh(digitCh1, digitCh2, digitCh3);
                    }
                    else
                    {
                        directLog("################# CONTROLLO FALLITO ################", 1);
                        result = false;
                    }
                    result = true;
                    break;
                }

            }

            digitCh1 = 3550;
            digitCh2 = 3550;
            digitCh3 = 3550;

            cs381.setCurrentCannelsDigit(digitCh1, digitCh2, digitCh3);

            Thread.Sleep(100);

            currents = testTool.getCurrents();
            
            directLog("", 1);
            directLog("CALIBRAZIONE a 20mA", 2);

            pidCh1 = new Pid(kph, kih, TestTool.OUTPUT_CURRENT_REFERENCE_HIGH);
            pidCh2 = new Pid(kph, kih, TestTool.OUTPUT_CURRENT_REFERENCE_HIGH);
            pidCh3 = new Pid(kph, kih, TestTool.OUTPUT_CURRENT_REFERENCE_HIGH);

            result = false;

            for (int i = 0; i < 100; i++)
            {

                if(i >= 99)
                {
                    directLog("################# TEST FALLITO PER TROPPE ITERAZIONI ################", 1);
                    result = false;
                    break;
                }
                float current1 = currents["I_CL2"];
                float current2 = currents["I_PH"];
                float current3 = currents["I_AUX"];


                int pidCh1out = (int)pidCh1.run(currents["I_CL2"]);
                int pidCh2out = (int)pidCh2.run(currents["I_PH"]);
                int pidCh3out = (int)pidCh3.run(currents["I_AUX"]);


                digitCh1 += pidCh1out;
                digitCh2 += pidCh2out;
                digitCh3 += pidCh3out;

                formatAndWriteOutput("I_CL2", current1, digitCh1, pidCh1out, 0);
                formatAndWriteOutput("I_PH", current2, digitCh2, pidCh2out, 0);
                formatAndWriteOutput("I_AUX", current3, digitCh3, pidCh3out, 1);

                cs381.setCurrentCannelsDigit(digitCh1, digitCh2, digitCh3);

                currents = testTool.getCurrents();

                if (chkCurrents(20F, current2, current3, TestTool.OUTPUT_CURRENT_REFERENCE_HIGH, TestTool.OUTPUT_CURRENT_TOLERANCE_HIGH))
                {
                    directLog("", 1);
                    directLog("Corrente I_CL2 in target -> " + current1.ToString() + " mA -> " + digitCh1.ToString() + " LSB", 1) ;
                    directLog("Corrente I_PH in target -> " + current2.ToString() + " mA -> " + digitCh2.ToString() + " LSB", 1);
                    directLog("Corrente I_AUX in target -> " + current3.ToString() + " mA -> " + digitCh3.ToString() + " LSB", 2);

                    directLog("VERIFICO TOLLERANZE DIGIT PUNTO ALTO", 0);

                    if (TestTool.checkResult(digitCh1, TestTool.OUTPUT_CURRENT_DIGIT_HIGH_REFERENCE, TestTool.OUTPUT_CURRENT_DIGIT_HIGH_TOLERANCE) && TestTool.checkResult(digitCh2, TestTool.OUTPUT_CURRENT_DIGIT_HIGH_REFERENCE, TestTool.OUTPUT_CURRENT_DIGIT_LOW_TOLERANCE) && TestTool.checkResult(digitCh3, TestTool.OUTPUT_CURRENT_DIGIT_HIGH_REFERENCE, TestTool.OUTPUT_CURRENT_DIGIT_HIGH_TOLERANCE))
                    {
                        directLog("-> OK", 1);
                        directLog("SCRIVO VALORI PUNTO ALTO SU REGISTRI DI CALIBRAZIONE", 2);
                        cs381.setOutputsCurrentsCalDigitHigh(digitCh1, digitCh2, digitCh3);
                    }
                    else
                    {
                        directLog("################# CONTROLLO FALLITO ################", 2);
                        result = false;
                    }



                    result = true;
                    break;
                }

            }
        }



        private void formatAndWriteOutput(string channell, float actualCurrent, int actualDigit, int pidOutput, int newLines)
        {
            directLog(channell + "->" + actualCurrent.ToString() + " mA [" + actualDigit.ToString() +"] -> PID OUTPUT: " + pidOutput.ToString() + " | " , newLines);
        }

        private bool chkCurrents(float current1, float current2, float current3,float target, float tolerance)
        {
            if (TestTool.checkResult(current1, target, tolerance) && TestTool.checkResult(current2, target, tolerance) && TestTool.checkResult(current3, target, tolerance))
            {
                return true;
            }

            return false;
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
