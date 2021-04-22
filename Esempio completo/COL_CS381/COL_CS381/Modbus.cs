namespace COL_CS381
{
    class Modbus
    {
        public static int ACTIVATED = 1;
        public static int DEACTIVATED = 0;
        public static int EXECUTE = 1;
        public static int RESET = 1;
        public static int READ = 0;
        public static bool LOW = false;
        public static bool HIGH = true;
        public static int CLOCKWISE = 1;
        public static int COUNTER_CLOCKWISE = 2;
        public static int STOP = 0;

        /* Discrete inputs */
        public static int DI_DI1_STATUS = 0;                 
          
        public static int C_RELAY1_STATUS = 0;                      /* Digital input 1 status */
        public static int C_RELAY2_STATUS = 1;                      /* Digital input 1 status */
        public static int C_RELAY3_STATUS = 2;                      /* Digital input 1 status */
        public static int C_RELAY4_STATUS = 3;                      /* Digital input 1 status */
        public static int C_RELAY_AUX1_STATUS = 4;                  /* Digital input 1 status */
        public static int C_RELAY_AUX2_STATUS = 5;                  /* Digital input 1 status */
        public static int C_RELAY_ALARM_STATUS = 6;                 /* Digital input 1 status */

        public static int IR_REAG1 = 0;                 
        public static int IR_REAG2 = 1;                  
        public static int IR_REAG3 = 2;                 
        public static int IR_H2O_FAULT = 3;

        public static int IR_PH = 0;
        public static int IR_RX = 1;
        public static int IR_TEMP = 2;
        public static int IR_I_IN_4_20mA = 3;

        public static int C_MOTOR0 = 7;
        public static int C_MOTOR1 = 8;

        public static int C_VDC_PT500 = 10;

        public static int MR_I_CH1_DIGIT = 4;
        public static int MR_I_CH2_DIGIT = 5;
        public static int MR_I_CH3_DIGIT = 6;
        public static int MR_I_CH4_DIGIT = 7;


        public static int MR_I_CH1_CAL_DIGIT_LOW = 12;
        public static int MR_I_CH2_CAL_DIGIT_LOW = 13;
        public static int MR_I_CH3_CAL_DIGIT_LOW = 14;
        public static int MR_I_CH4_CAL_DIGIT_LOW = 15;


        public static int MR_I_CH1_CAL_DIGIT_HIGH = 8;
        public static int MR_I_CH2_CAL_DIGIT_HIGH = 9;
        public static int MR_I_CH3_CAL_DIGIT_HIGH = 10;
        public static int MR_I_CH4_CAL_DIGIT_HIGH = 11;

        public static int MR_TEST_SERIAL_PORT = 16;

        public static int MR_TEST_FLASH = 17;
        public static int MR_EEPROM = 18;



        public static int C_ANA_GP0 = 11;
        public static int C_ANA_GP1 = 12;


    }
}
