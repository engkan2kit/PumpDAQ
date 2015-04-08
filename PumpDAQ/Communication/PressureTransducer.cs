using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpDAQ
{
    class PressureTransducer
    {
        public String ID;
        public int Well=0;
        public double DistanceFromPump=0;
        public double Pressure = 0;
        public double Density = 1;
        public double Gravity = 9.81;
        public double ATM = 101325;
        public PressureTransducer()
        {
            this.ID = "";
        }
        public PressureTransducer(String ID)
        {
            this.ID = ID;
        }

        public double readLevel()
        {
            return ((this.Pressure-this.ATM) / (this.Gravity*this.Density));
        }
    }
}
