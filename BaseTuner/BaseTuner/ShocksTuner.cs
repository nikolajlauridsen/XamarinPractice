using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTuner
{
    public class ShocksTuner : SpringsTuner
    {

        public ShocksTuner() { }

        public double GetRebound(double wRatio)
        {
            return this.GetBaseValue(wRatio) * 0.60;
        }
    }
}
