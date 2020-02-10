using System;

namespace BaseTuner
{
    public class SpringsTuner
    {
        public double MinValue = 55.0;
        public double MaxValue = 215.0;

        public double BaseValue
        {
            get
            {
                return 0.0;
            }
        }

        public SpringsTuner()
        {
        }

        public double GetBaseValue(double weightRatio)
        {
            return (MaxValue - MinValue) * weightRatio + MinValue;
        }
    }
}
