using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using EasyModbus;

namespace COL_CS381
{
    class CS381
    {
        ModbusClient board;
        SerialPort serial;
        bool communicationState = false;

        public CS381(SerialPort _serial)
        {
            this.serial = _serial;
            board = new ModbusClient(serial.PortName);
            connectBoardToTest();
        }

        public void connectBoardToTest()
        {
            if (serial.IsOpen) serial.Close();
            if (!board.Connected)
            {
                board = new ModbusClient(serial.PortName);
                board.UnitIdentifier = 1; //Not necessary since default slaveID = 1;
                board.Baudrate = 9600;
                
                board.Parity = System.IO.Ports.Parity.None;
                board.StopBits = System.IO.Ports.StopBits.One;

                //modbusClient.ConnectionTimeout = 500;
                try
                {
                    if (!board.Connected)
                    {
                        board.Connect();
                        communicationState = true; ;
                    }
                }
                catch (Exception ex)
                {
                    communicationState = false;
                }
            }
        }


        public void setDigitalOutput(int register, bool value)
        {
            board.WriteSingleCoil(register, value);
        }


        public void setMotor(int register, bool value)
        {
            board.WriteSingleCoil(register, value);
        }

        public void setGPIO(int register, bool value)
        {
            board.WriteSingleCoil(register, value);
        }

        public void getAnalogInput(int register)
        {
            int[] value = board.ReadInputRegisters(4, 6);
            
            UInt16 temp = (UInt16)value[3];
            byte[] b = BitConverter.GetBytes(temp);

        }

        public void startTest(int register, int value)
        {
            board.WriteMultipleRegisters(register,new int[] { value });
        }

        public int getTestResult(int register)
        {
            int result = board.ReadHoldingRegisters(register,1)[0];
            return result;
        }

        public void setVdcPT500(bool value)
        {
            board.WriteSingleCoil(Modbus.C_VDC_PT500, value);
        }


        public void testCellSerialPort()
        {
            board.WriteMultipleRegisters(Modbus.MR_TEST_SERIAL_PORT, new int[] { Modbus.EXECUTE }) ;
        }

        public void setCurrentCannelsDigit(int ch1, int ch2, int ch3)
        {
            board.WriteMultipleRegisters(Modbus.MR_I_CH1_DIGIT, new int[] { ch1, ch2, ch3});
        }


        public void setOutputsCurrentsCalDigitLow(int dicCh1, int dicCh2, int dicCh3)
        {
            board.WriteMultipleRegisters(Modbus.MR_I_CH1_CAL_DIGIT_LOW, new int[] { dicCh1, dicCh2, dicCh3 });
        }

        public void setOutputsCurrentsCalDigitHigh(int dicCh1, int dicCh2, int dicCh3)
        {
            board.WriteMultipleRegisters(Modbus.MR_I_CH1_CAL_DIGIT_HIGH, new int[] { dicCh1, dicCh2, dicCh3 });
        }

        public Dictionary<string, int> getAnalogInputs()
        {

            /*           
                00 Cloro linero (da dove si misura))
                01 pH x 100 (J2 - Il controllo inbisce la lettura?)
                02 Cloro totale
                03 Temperatura x 100
                04 Redox
                05 Conducibilità            
            */

            Dictionary<string, int> analogInputs = new Dictionary<string, int>();

            analogInputs.Add("PH", board.ReadInputRegisters(Modbus.IR_PH, 1)[0]);
            analogInputs.Add("RX", board.ReadInputRegisters(Modbus.IR_RX, 1)[0]);
            analogInputs.Add("TEMP", board.ReadInputRegisters(Modbus.IR_TEMP, 1)[0]);
            analogInputs.Add("I_IN_4-20mA", board.ReadInputRegisters(Modbus.IR_H2O_FAULT, 1)[0]);

            return analogInputs;
        }


        public Dictionary<string, bool> getDigitalInputs()
        {
            /*           
              REAG_1 14
              REAG_2 15
              IN_STBY 16
            */

            Dictionary<string, bool> digitalInputs = new Dictionary<string, bool>();

            digitalInputs.Add("DI_REAG1", board.ReadDiscreteInputs(Modbus.IR_REAG1, 1)[0]);
            digitalInputs.Add("DI_REAG2", board.ReadDiscreteInputs(Modbus.IR_REAG2, 1)[0]);
            digitalInputs.Add("DI_REAG3", board.ReadDiscreteInputs(Modbus.IR_REAG3, 1)[0]);
            digitalInputs.Add("DI_H2O_FAULT", board.ReadDiscreteInputs(Modbus.IR_H2O_FAULT, 1)[0]);
      
            return digitalInputs;
        }



        public Dictionary<string, bool> getKeyboard() 
        {
            Dictionary<string, bool> keys = new Dictionary<string, bool>();

            keys.Add("SU", board.ReadDiscreteInputs(7,1)[0]);
            keys.Add("INVIO", board.ReadDiscreteInputs(5, 1)[0]);
            keys.Add("USCITA", board.ReadDiscreteInputs(6, 1)[0]);
            keys.Add("GIU", board.ReadDiscreteInputs(4, 1)[0]);

            return keys;
        }




    }
}
    