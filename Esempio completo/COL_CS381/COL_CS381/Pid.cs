using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL_CS381
{


    class Pid
    {
        float ki = 0;
        float kp = 0;
        float acc = 0;
        float target = 0;

        public Pid(float _kp, float _ki, float _target)
        {
            this.ki = _ki;
            this.kp = _kp;
            this.target = _target;
        }

        public float run(float value)
        {
            acc += target - value;
            if (acc > 100) acc = 1000;
            if (acc < -100) acc = -1000;
            float pidValue = kp * (target - value) + ki * acc;
            return pidValue;
        }
    }
}
