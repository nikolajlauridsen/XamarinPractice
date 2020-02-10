using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTuner
{
    public class Car
    {
        public double WeightRatio = 0.50;
        public SpringsTuner Suspension = new SpringsTuner();
        public ShocksTuner Shocks = new ShocksTuner();
    }
}
