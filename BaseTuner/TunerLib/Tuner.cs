using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TunerLib
{
    public class Tuner : INotifyPropertyChanged
    {
        private Dictionary<string, string[]> depMap = new Dictionary<string, string[]>
        {
            { "WeightRatio", new[] {"RollFront", "RollRear", "SpringsFront", "SpringsRear", "CompFront", "CompRear"} },
            {"RollMin", new[]{"RollFront", "RollRear" } },
            {"RollMax", new[]{"RollFront", "RollRear" } },
            {"SpringsMin", new[]{"SpringsFront", "SpringsRear" } },
            {"SpringsMax", new[]{"SpringsFront", "SpringsRear" } },
            {"CompMin", new[]{"CompFront", "CompRear" } },
            {"CompMax", new[]{"CompFront", "CompRear" } },
            {"CompFront", new[]{"ReboundFront"} },
            {"CompRear", new[]{"ReboundRear"} }
        };
        public event PropertyChangedEventHandler PropertyChanged;
        public double WeightRatio { get; set; }
        public double RollMin { get; set; }
        public double RollMax { get; set; }

        public double RollFront { 
            get 
            {
                return calcBase(RollMin, RollMax, WeightRatio);
            } 
        }
        public double RollRear {
            get
            {
                return calcBase(RollMin, RollMax, (1 - WeightRatio));
            }
        }

        public double SpringsMin { get; set; }
        public double SpringsMax { get; set; }
        public double SpringsFront {
            get
            {
                return calcBase(SpringsMin, SpringsMax, WeightRatio);
            }
        }
        public double SpringsRear { 
            get 
            {
                return calcBase(SpringsMin, SpringsMax, (1 - WeightRatio));
            } 
        }

        public double CompMin { get; set; }
        public double CompMax { get; set; }
        private double _compFront;

        public double CompFront { get; set; }
        public double CompRear { get; set; }
        public double ReboundFront { get; set; }
        public double ReboundRear { get; set; }

        public Tuner()
        {
            WeightRatio = 0.50;

            RollMin = 50.5;
            RollMax = 215.5;

            SpringsMin = 50.5;
            SpringsMax = 215.5;

            CompMin = 50.5;
            CompMax = 215.5;
        }

        private double calcBase(double min, double max, double ratio)
        {
            return (max - min) * ratio + min;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (depMap.ContainsKey(propertyName))
            {
                foreach(string p in depMap[propertyName])
                {
                    NotifyPropertyChanged(p);
                }
            }
        }


    }
}
