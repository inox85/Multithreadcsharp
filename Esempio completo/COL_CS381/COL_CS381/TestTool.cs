using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace COL_CS381
{
    class TestTool
    {

        public static string ALL_LOW = "0";
        public static string READ_ANALOG_INPUTS = "1";
        public static string READ_DIGITAL_INPUTS = "2";
        public static string SET_REFERENCE_HIGH = "3";
        public static string SET_REFERENCE_LOW = "4";
        public static string SET_CURRENT_VERIFY = "5";
        public static string SET_DIGITAL_INPUT_1 = "6";
        public static string SET_DIGITAL_INPUT_2 = "7";
        public static string SET_DIGITAL_INPUT_3 = "8";
        public static string SET_ALL_DIGITAL_INPUTS_HIGH = "9";
        public static string KEYBOARD_TEST = "K";
        public static string BEEP = "B";
        public static string BEEP_SEQ = "A";
        public static string BEEP_LONG = "L";



        public static float MAIN_24VDC_REFERENCE = 24F;
        public static float MAIN_24VDC_TOLERANCE = 2F;

        public static float MAIN_5VDC_REFERENCE = 5F;
        public static float MAIN_5VDC_TOLERANCE = 0.5F;

        public static float MAIN_5VDC_BKP_REFERENCE = 4.5F;
        public static float MAIN_5VDC_BKP_TOLERANCE = 1F;

        public static float MOTOR_REFERENCE = 24F;
        public static float MOTOR_ZERO = 0F;
        public static float MOTOR_TOLERANCE = 3F; 

        public static int PH_REFERENCE_HIGH = 3654;
        public static int PH_REFERENCE_LOW = 0;
        public static int PH_TOLERANCE = 100;

        public static int RX_REFERENCE_HIGH = 1407;
        public static int RX_REFERENCE_LOW = 0;
        public static int RX_TOLERANCE = 100;

        public static int TEMP_REFERENCE = 826;
        public static int TEMP_TOLERANCE = 50;

        public static int I_IN_4_20mA_REFERENCE_LOW = 826;
        public static int I_IN_4_20mA_REFERENCE_HIGH = 4045;
        public static int I_IN_4_20mA_TOLERANCE = 100;

        public static float INPUT_CURRENT_REFERENCE_HIGH = 14.16F;
        public static float INPUT_CURRENT_REFERENCE_LOW = 7.10F;
        public static float INPUT_CURRENT_REFERENCE_VERIFY = 8.34F;
        public static float INPUT_CURRENT_TOLERANCE = 1F;
        public static float INPUT_CURRENT_TOLERANCE_VERIFY = 0.1F;

        public static float OUTPUT_CURRENT_REFERENCE_HIGH = 20F;
        public static float OUTPUT_CURRENT_TOLERANCE_HIGH = 0.01F;
        public static float OUTPUT_CURRENT_REFERENCE_LOW = 0.02F;
        public static float OUTPUT_CURRENT_TOLERANCE_LOW = 0.01F;

        public static int OUTPUT_CURRENT_DIGIT_HIGH_REFERENCE = 3550;
        public static int OUTPUT_CURRENT_DIGIT_LOW_REFERENCE = 365;
        public static int OUTPUT_CURRENT_DIGIT_HIGH_TOLERANCE = 50;
        public static int OUTPUT_CURRENT_DIGIT_LOW_TOLERANCE = 50;

        public static string[] analogChannels = { "I_CL2", "I_PH", "I_AUX", "I_CH4", "VDC_24V", "VDC_5V", "VDC_5V_BKP", "VDC_5V_EXP", "MOTOR_0", "MOTOR_1", "VDC_PT500"};
        public static string[] digitalInputs = { "REAG_1", "REAG_2", "IN_STBY" };
        public static string[] digitalOutputs = { "S1_CL", "S2_CL", "S1_PH", "S2_PH", "ALARM_NO", "S1_AUX", "S1_RX", "ALARM_NC", "GPIO0", "GPIO1" };

        SerialPort serial;

        public TestTool(SerialPort _serial)
        {
            this.serial = _serial;
            if (!serial.IsOpen) serial.Open();
        }

        public Dictionary<string, float> getAnalogInputs()
        {
            Dictionary<string, float> values = new Dictionary<string, float>();

            string[] valuesString = readValues(READ_ANALOG_INPUTS);

            for (int i = 0; i < analogChannels.Length; i++)
            { 
                values.Add(analogChannels[i], float.Parse(valuesString[i])); 
            }

            return values;
        }


        public Dictionary<string, bool> getDigitalOutputs()
        {
            Dictionary<string, bool> values = new Dictionary<string, bool>();

            string[] valuesString = readValues(READ_DIGITAL_INPUTS);

            for (int i = 0; i < digitalOutputs.Length; i++)
            {
                if (valuesString[i] == "1")
                {
                    values.Add(digitalOutputs[i], false);
                }

                if (valuesString[i] == "0")
                {
                    values.Add(digitalOutputs[i], true);
                }

            }

            return values;
        }


        public void beep()
        {
            serial.Write(BEEP);
        }


        public  void beep_seq()
        {
            serial.Write(BEEP_SEQ);
        }

        public  void beep_long()
        {
            serial.Write(BEEP_LONG);
        }

        public Dictionary<string,float> getVoltages()
        {
            var values = getAnalogInputs();

            Dictionary<string, float> voltages = new Dictionary<string, float>();

            voltages.Add("VDC_24V",(float)Math.Round(values["VDC_24V"] / 1000,2));
            voltages.Add("VDC_5V", (float)Math.Round(values["VDC_5V"] / 1000, 2));
            voltages.Add("VDC_5V_BKP", (float)Math.Round(values["VDC_5V_BKP"] / 1000, 2));
            voltages.Add("VDC_5V_EXP", (float)Math.Round(values["VDC_5V_EXP"] / 1000, 2));
            voltages.Add("VDC_PT500", (float)Math.Round(values["VDC_PT500"] / 1000, 2));

            return voltages;
        }

        public Dictionary<string, float> getCurrents()
        {
            var values = getAnalogInputs();

            Dictionary<string, float> currents = new Dictionary<string, float>();
            
            currents.Add("I_CL2", (float)Math.Round(values["I_CL2"] / 1000, 3));
            currents.Add("I_PH", (float)Math.Round(values["I_PH"] / 1000, 3));
            currents.Add("I_AUX", (float)Math.Round(values["I_AUX"] / 1000, 3));

            return currents;
        }

        public void keyboardTest()
        {
            if (!serial.IsOpen) serial.Open();
            serial.Write(KEYBOARD_TEST);
            Thread.Sleep(100);
        }



        public Dictionary<string, float> getMotors()
        {
            var values = getAnalogInputs();

            Dictionary<string, float> motors = new Dictionary<string, float>();

            motors.Add("MOTOR_0", (float)Math.Round(values["MOTOR_0"] / 1000, 2));
            motors.Add("MOTOR_1", (float)Math.Round(values["MOTOR_1"] / 1000, 2));

            return motors;
        }

        private string[] readValues(string par)
        {
            try
            {
                if (!serial.IsOpen) serial.Open();
                serial.DiscardInBuffer();
                serial.Write(par);
                Thread.Sleep(500);
                string val = serial.ReadExisting().Split('\r')[0];
                return val.Replace(".", ",").Split(';');
            }
            catch
            {
                MessageBox.Show("Problemi di comunicazione con il tool di collaudo, assicurarsi che la porta selezionata sia corretta e che il tool di collaudo sia correttamente alimentato");
                return null;
            }
        }

        public static bool checkResult(float value, float target, float tolerance)
        {
            if (Math.Abs(value - target) < tolerance) return true;

            return false;
        }

        public static string formatResult(string feature, float target, float tolerance, float value, bool result)
        {
            string testResult = "FALLITO";
            if(result) testResult = "PASSATO";

            string fortattedString = feature + " [TARGET: " + target + " ] -> [ MISURATO: " + value + " ] -> [DEV: " + (value - target).ToString() + "], [MAX DEV: " + (tolerance).ToString() + "]  -> [RISULTATO: " + testResult + " ] ";

            return fortattedString;
        }

        public static string assertTrue(bool result)
        {

            if (result)
            {
                
                return "=================== TEST SUPERATO =================== ";
            }

            return "################# TEST FALLITO #################";
        }

        public bool send(string cmd)
        {
            try
            {
                if (!serial.IsOpen) serial.Open();
                serial.DiscardInBuffer();
                serial.Write(cmd);
                Thread.Sleep(100);
                string echo = serial.ReadExisting().Replace("\r\n", "");
                if (echo.Contains(cmd))
                {
                    return true;
                }
                MessageBox.Show("ATTENZIONE: Nessuna risposta dal sistema di collaudo, verificare la connessione e che la porta selezionata sia corretta.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ATTENZIONE: Nessuna risposta dal sistema di collaudo, verificare la connessione e che la porta selezionata sia corretta.");
                return false;
            }
        }

        
    }
}
