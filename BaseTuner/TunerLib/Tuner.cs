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
            { "WeightRatio", new[] {"RollFront", "RollRear", "SpringsFront", "SpringsRear", "ReboundFront", "ReboundRear"} },
            {"RollMin", new[]{"RollFront", "RollRear" } },
            {"RollMax", new[]{"RollFront", "RollRear" } },
            {"SpringsMin", new[]{"SpringsFront", "SpringsRear" } },
            {"SpringsMax", new[]{"SpringsFront", "SpringsRear" } },
            {"ReboundMin", new[]{"ReboundFront", "ReboundRear" } },
            {"ReboundMax", new[]{"ReboundFront", "ReboundRear" } },
            {"ReboundFront", new[]{"BumpFront"} },
            {"ReboundRear", new[]{"BumpRear"} }
        };
        public event PropertyChangedEventHandler PropertyChanged;

        private double _weightRatio;
        public double WeightRatio { 
            get => _weightRatio;
            set 
            {
                _weightRatio = value;
                NotifyPropertyChanged();
            } 
        }
        private double _rollMin;
        public double RollMin {
            get => _rollMin;
            set 
            {
                _rollMin = value;
                NotifyPropertyChanged();
            } 
        }

        private double _rollMax;
        public double RollMax {
            get => _rollMax;
            set
            {
                _rollMax = value;
                NotifyPropertyChanged();
            }
        }

        public double RollFront => calcBase(RollMin, RollMax, WeightRatio);
        public double RollRear => calcBase(RollMin, RollMax, (1 - WeightRatio));

        private double _springsMin;
        public double SpringsMin {
            get => _springsMin;
            set
            {
                _springsMin = value;
                NotifyPropertyChanged();
            }
        }

        private double _springsMax;
        public double SpringsMax {
            get => _springsMax;
            set
            {
                _springsMax = value;
                NotifyPropertyChanged();
            } 
        }
        public double SpringsFront => calcBase(SpringsMin, SpringsMax, WeightRatio);
        public double SpringsRear => calcBase(SpringsMin, SpringsMax, (1 - WeightRatio));

        private double _reboundMin;
        public double ReboundMin { get => _reboundMin;
            set
            {
                _reboundMin = value;
                NotifyPropertyChanged();
            } 
        }

        private double _reboundMax;
        public double ReboundMax { get => _reboundMax;
            set
            {
                _reboundMax = value;
                NotifyPropertyChanged();
            } 
        }

        public double ReboundFront => calcBase(ReboundMin, ReboundMax, WeightRatio);
        public double ReboundRear => calcBase(ReboundMin, ReboundMax, (1 - WeightRatio));
        public double BumpFront => ReboundFront * 0.6;
        public double BumpRear => ReboundRear * 0.6;

        public Tuner()
        {
            WeightRatio = 0.50;

            RollMin = 1;
            RollMax = 65;

            SpringsMin = 37.5;
            SpringsMax = 187.5;

            ReboundMin = 3;
            ReboundMax = 20;
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
