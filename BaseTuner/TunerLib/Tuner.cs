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
            {"CompFront", new[]{"BumpFront"} },
            {"CompRear", new[]{"BumpRear"} }
        };
        public event PropertyChangedEventHandler PropertyChanged;

        private double _weightRatio;
        public double WeightRatio { 
            get => _weightRatio;
            set 
            {
                _weightRatio = value;
                NotifyPropertyChanged("WeightRatio");
            } 
        }
        private double _rollMin;
        public double RollMin {
            get => _rollMin;
            set 
            {
                _rollMin = value;
                NotifyPropertyChanged("RollMin");
            } 
        }

        private double _rollMax;
        public double RollMax {
            get => _rollMax;
            set
            {
                _rollMax = value;
                NotifyPropertyChanged("RollMax");
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
                NotifyPropertyChanged("SpringsMin");
            }
        }

        private double _springsMax;
        public double SpringsMax {
            get => _springsMax;
            set
            {
                _springsMax = value;
                NotifyPropertyChanged("SpringsMax");
            } 
        }
        public double SpringsFront => calcBase(SpringsMin, SpringsMax, WeightRatio);
        public double SpringsRear => calcBase(SpringsMin, SpringsMax, (1 - WeightRatio));

        private double _compMin;
        public double CompMin { get { return _compMin; }
            set
            {
                _compMin = value;
                NotifyPropertyChanged("CompMin");
            } 
        }

        private double _compMax;
        public double CompMax { get { return _compMax; }
            set
            {
                _compMax = value;
                NotifyPropertyChanged("CompMax");
            } 
        }

        public double CompFront => calcBase(CompMin, CompMax, WeightRatio);
        public double CompRear => calcBase(CompMin, CompMax, (1 - WeightRatio));
        public double BumpFront => CompFront * 0.6;
        public double BumpRear => CompRear * 0.6;

        public Tuner()
        {
            WeightRatio = 0.50;

            RollMin = 1;
            RollMax = 65;

            SpringsMin = 37.5;
            SpringsMax = 187.5;

            CompMin = 3;
            CompMax = 20;
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
