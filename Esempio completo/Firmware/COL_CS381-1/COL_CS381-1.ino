//#define DEBUG
//Canali analogici per Ph e ORP/
//Il collaudo abilita o disabilita un GPIO che applica un riferimento di tensione all'ingresso del canale analogico della scheda, i canali sono anche PWM e sull'uscita è presente un filtro

#define BUZZER 7

#define VERIFY_POINT 0
#define LOW_POINT 1
#define HIGH_POINT 2

#define ORP_REF 10
#define PH_REF 11

//Controllo dei relays

//Applicano livelli di resisenza all'ingresso della scheda espansione
#define EXP_TEMP 14 
#define EXP_CONT 15

//Cortocircuita l'ingresso FLUX con il +24VDC

#define FLUX 16

//Alimenta o meno la scheda sotto test

#define MAIN_SUPPLY 17

//Misura della tensione di alimentazione
//Misurano il +24VDC attraverso dei partitori resistivi

#define VDC_24V A4
#define VDC_5V A5
#define VDC_5V_EXP A12
#define VDC_5V_BKP A6

#define VDC_PT500 A13

//Ingressi analogici per lettura uscite motori

#define MOTOR_0 A3
#define MOTOR_1 A2

//Test tastiera

#define KEYBOARD_REFERENCE 37
#define KEYBOARD_SU 29
#define KEYBOARD_USCITA 31
#define KEYBOARD_INVIO 33
#define KEYBOARD_GIU 35

//Lettura uscite in corrente della scheda sotto test

#define I_CL2 A8
#define I_PH A9
#define I_AUX A10
#define I_CH4 A11

//Ingressi digitali

#define REAG_1 14
#define REAG_2 15
#define IN_STBY 16

//Uscite digitali

#define S1_CL 53
#define S2_CL 51
#define S1_PH 49
#define S2_PH 47
#define ALARM_NO 39 
#define ALARM_NC 41 
#define S1_AUX 45 
#define SI_RX 43 

//GPIO connettore espansione (SDA, SCL)

#define GPIO0 12
#define GPIO1 13

//Generatori di corrente

#define CURRENT_LOW 5
#define CURRENT_VER 4
#define CURRENT_HIGH 3

//Coefficenti per tensione e corrente 

#define VDC_24V_COEFF 26.62
#define VDC_5V_COEFF 4.94
#define I_CH_COEFF 20.11
#define I_CH_OFFSET 0

//Coefficenti per tensione e corrente 

#define GET_24VDC 1
#define GET_MOTOR_OUTPUTS 2
#define GET_CURRENT_OUTPUTS 3

#define ALL_LOW '0'
#define READ_ANALOG_INPUTS '1'
#define READ_DIGITAL_OUTPUTS '2'
#define SET_REFERENCE_HIGH '3'
#define SET_REFERENCE_LOW '4'
#define SET_CURRENT_VERIFY '5'
#define SET_DIGITAL_INPUT_1 '6'
#define SET_DIGITAL_INPUT_2 '7'
#define SET_DIGITAL_INPUT_3 '8'
#define SET_ALL_DIGITAL_INPUTS_HIGH '9'
#define KEYBOARD_TEST 'K'
#define BEEP 'B'
#define BEEP_SEQ 'A'
#define BEEP_LONG 'L'


#define TEST 't'

int ANALOG_INPUTS[] = {I_CL2, I_PH, I_AUX, I_CH4, VDC_24V, VDC_5V,VDC_5V_BKP,VDC_5V_EXP, VDC_5V_EXP, MOTOR_0, MOTOR_1};
int DIGITAL_INPUTS[] = {REAG_1, REAG_2, IN_STBY};
int DIGITAL_OUTPUTS[] = {S1_CL, S2_CL, S1_PH, S2_PH, ALARM_NO,S1_AUX, SI_RX, ALARM_NC,GPIO0, GPIO1};

void setup() {

  Serial.begin(9600);

  analogReference(INTERNAL2V56);
 
  pinMode(REAG_1, OUTPUT);
  pinMode(REAG_2, OUTPUT);
  pinMode(IN_STBY, OUTPUT);
  digitalWrite(REAG_1, LOW);
  digitalWrite(REAG_2, LOW);
  digitalWrite(IN_STBY, LOW);

  pinMode(ORP_REF, OUTPUT);
  pinMode(PH_REF, OUTPUT);
  digitalWrite(ORP_REF, LOW);
  digitalWrite(PH_REF, LOW);
  
  pinMode(CURRENT_LOW, OUTPUT);
  pinMode(CURRENT_VER, OUTPUT);
  pinMode(CURRENT_HIGH, OUTPUT);   
  digitalWrite(CURRENT_LOW, LOW);
  digitalWrite(CURRENT_VER, LOW);
  digitalWrite(CURRENT_HIGH, LOW);

  pinMode(S1_CL, INPUT);
  pinMode(S2_CL, INPUT);
  pinMode(S1_PH, INPUT);
  pinMode(S2_PH, INPUT);
  pinMode(ALARM_NO, INPUT);
  pinMode(ALARM_NC, INPUT);
  pinMode(S1_AUX, INPUT);
  pinMode(SI_RX, INPUT);
  pinMode(GPIO0, INPUT);
  pinMode(GPIO1, INPUT);
  
  pinMode(KEYBOARD_REFERENCE, OUTPUT);
  digitalWrite(KEYBOARD_REFERENCE, LOW);

  pinMode(KEYBOARD_SU, OUTPUT);
  digitalWrite(KEYBOARD_SU, HIGH);
  
  pinMode(KEYBOARD_USCITA, OUTPUT);
  digitalWrite(KEYBOARD_USCITA, HIGH);
  
  pinMode(KEYBOARD_INVIO, OUTPUT);
  digitalWrite(KEYBOARD_INVIO, HIGH);
  
  pinMode(KEYBOARD_GIU, OUTPUT);
  digitalWrite(KEYBOARD_GIU, HIGH);

  pinMode(BUZZER, OUTPUT);
  digitalWrite(BUZZER, LOW);
  
}

void loop() {
  // put your main code here, to run repeatedly:

}

void serialEvent() {

  if(Serial.available() > 0)
  {
    char cmd = Serial.read();

    #ifdef DEBUG
    Serial.print("Comando ricevuto: ");
    Serial.println(cmd);
    #endif

    switch(cmd) 
    {
      
      case ALL_LOW:                           // cmd: 0
        setDigitalInputs(0);
        setCurrentGenerator(LOW_POINT);
        setPhOrpReference(LOW_POINT);
        Serial.println(cmd);
        break;
      
      case READ_ANALOG_INPUTS:                // cmd: 1
        readAnalogInputs();
        Serial.println(cmd);
        break;
        
      case READ_DIGITAL_OUTPUTS:              // cmd: 2
        getDigitalOutputs();
        Serial.println(cmd);
        break;
        
      case SET_REFERENCE_HIGH:                // cmd: 3
        setCurrentGenerator(HIGH_POINT);
        setPhOrpReference(HIGH_POINT);
        Serial.println(cmd);
        break;

      case SET_REFERENCE_LOW:                  // cmd: 4
        setCurrentGenerator(LOW_POINT);
        setPhOrpReference(LOW_POINT);
        Serial.println(cmd);
        break;

      case SET_CURRENT_VERIFY:                 // cmd: 5
        setCurrentGenerator(VERIFY_POINT);
        Serial.println(cmd);
        break;

      case SET_DIGITAL_INPUT_1:                // cmd: 6
        setDigitalInputs(1);
        Serial.println(cmd);
        break;

      case SET_DIGITAL_INPUT_2:                // cmd: 7
        setDigitalInputs(2);
        Serial.println(cmd);
        break;

      case SET_DIGITAL_INPUT_3:                // cmd: 8
        setDigitalInputs(3);
        Serial.println(cmd);
        break;

      case SET_ALL_DIGITAL_INPUTS_HIGH:        // cmd: 9
        setDigitalInputs(4);
        Serial.println(cmd);
        break;

      case KEYBOARD_TEST:        // cmd: K
        keyboardTest();
        break;  
        
      case BEEP:        // cmd: K
        beep();
        break;
        
      case BEEP_SEQ:        // cmd: K
        beep_seq();
        break;

       case BEEP_LONG:        // cmd: K
        beep_long();
        break;  

       default:
        // statements
        break;
    }
    
    //Serial.println(cmd);
  }
  
}


void beep()
{
  digitalWrite(BUZZER, HIGH);
  delay(50);
  digitalWrite(BUZZER, LOW);
}

void beep_seq()
{
  digitalWrite(BUZZER, HIGH);
  delay(20);
  digitalWrite(BUZZER, LOW);
  delay(20);
  digitalWrite(BUZZER, HIGH);
  delay(20);
  digitalWrite(BUZZER, LOW);
  delay(20);
  digitalWrite(BUZZER, HIGH);
  delay(20);
  digitalWrite(BUZZER, LOW);
}

void beep_long()
{
  digitalWrite(BUZZER, HIGH);
  delay(500);
  digitalWrite(BUZZER, LOW);
}

//Gestine delle uscite in corrente della scheda sotto test
void readAnalogInputs()
{
    String values = "";
    
    values += (String)readCurrent(A8) + ";";
    values += (String)readCurrent(A9) + ";";
    values += (String)readCurrent(A10) + ";";
    values += (String)readCurrent(A11) + ";";
    values += (String)readVoltage(VDC_24V,VDC_24V_COEFF) + ";";
    values += (String)readVoltage(VDC_5V, VDC_5V_COEFF) + ";";
    values += (String)readVoltage(VDC_5V_BKP,VDC_5V_COEFF) + ";";
    values += (String)readVoltage(VDC_5V_EXP,VDC_5V_COEFF) + ";";
    values += (String)readVoltage(MOTOR_0,VDC_24V_COEFF) + ";";
    values += (String)readVoltage(MOTOR_1,VDC_24V_COEFF) + ";";
    values += (String)readVoltage(VDC_PT500,VDC_24V_COEFF) + ";";
    Serial.println(values);
}


double readCurrent(int ch)
{
    double value = 0;
    
    for(int i = 0; i<10; i++)
    {
       value += analogRead(ch);
    }

    return ((value*I_CH_COEFF) + I_CH_OFFSET)/10;   
}


void keyboardTest()
{
  delay(300);
  digitalWrite(KEYBOARD_REFERENCE, LOW);
  digitalWrite(KEYBOARD_SU, HIGH);
  digitalWrite(KEYBOARD_USCITA, HIGH);
  digitalWrite(KEYBOARD_INVIO, HIGH);
  digitalWrite(KEYBOARD_GIU, HIGH);
  delay(300);
  digitalWrite(KEYBOARD_SU, LOW);
  digitalWrite(KEYBOARD_USCITA, HIGH);
  digitalWrite(KEYBOARD_INVIO, HIGH);
  digitalWrite(KEYBOARD_GIU, HIGH);
  delay(300);
  digitalWrite(KEYBOARD_SU, HIGH);
  digitalWrite(KEYBOARD_USCITA, LOW);
  digitalWrite(KEYBOARD_INVIO, HIGH);
  digitalWrite(KEYBOARD_GIU, HIGH);
  delay(300);
  digitalWrite(KEYBOARD_SU, HIGH);
  digitalWrite(KEYBOARD_USCITA, HIGH);
  digitalWrite(KEYBOARD_INVIO, LOW);
  digitalWrite(KEYBOARD_GIU, HIGH);
  delay(300);
  digitalWrite(KEYBOARD_SU, HIGH);
  digitalWrite(KEYBOARD_USCITA, HIGH);
  digitalWrite(KEYBOARD_INVIO, HIGH);
  digitalWrite(KEYBOARD_GIU, LOW);  
  delay(300);
  digitalWrite(KEYBOARD_REFERENCE, LOW);
  digitalWrite(KEYBOARD_SU, HIGH);
  digitalWrite(KEYBOARD_USCITA, HIGH);
  digitalWrite(KEYBOARD_INVIO, HIGH);
  digitalWrite(KEYBOARD_GIU, HIGH);
  delay(300);
  
}

double readVoltage(int ch, double vCoeff)
{
    double value = 0;
    for(int i = 0; i<10; i++)
    {
       value += analogRead(ch);
    }
    
    value  = value/10;
    
    return (value*vCoeff);   
}

void setCurrentGenerator(int value){

  if(value == LOW_POINT)
  {
    digitalWrite(CURRENT_LOW, HIGH);
    digitalWrite(CURRENT_VER, LOW);
    digitalWrite(CURRENT_HIGH, LOW);
  }
  
  
  if(value == HIGH_POINT)
  {
    digitalWrite(CURRENT_LOW, HIGH);
    digitalWrite(CURRENT_VER, HIGH);
    digitalWrite(CURRENT_HIGH, HIGH);
  }
  
  
  if(value == VERIFY_POINT)
  {
    digitalWrite(CURRENT_LOW, LOW);
    digitalWrite(CURRENT_VER, HIGH);
    digitalWrite(CURRENT_HIGH, HIGH);
  }

}


void setPhOrpReference(int value){

  if(value == LOW_POINT)
  {
    digitalWrite(ORP_REF, LOW);
    digitalWrite(PH_REF, LOW);

  }
  
  if(value == HIGH_POINT)
  {
    digitalWrite(ORP_REF, HIGH);
    digitalWrite(PH_REF, HIGH);
  }

}

void getDigitalOutputs(){

 String result = "";
 
 for(int i = 0; i < 10; i++)
 {
   result += (String)digitalRead(DIGITAL_OUTPUTS[i]) + ";";
 }

 Serial.println(result);
}


void setDigitalInputs(int value)
{
  digitalWrite(REAG_1, LOW);
  digitalWrite(REAG_2, LOW);
  digitalWrite(IN_STBY, LOW);

  if(value == 1)
  {
    digitalWrite(REAG_1, HIGH);
  }

  if(value == 2)
  {
    digitalWrite(REAG_2, HIGH);
  }

  if(value == 3)
  {
    digitalWrite(IN_STBY, HIGH);
  }

  if(value == 4)
  {
    digitalWrite(REAG_1, HIGH);
    digitalWrite(REAG_2, HIGH);
    digitalWrite(IN_STBY, HIGH);
  }
  
}
