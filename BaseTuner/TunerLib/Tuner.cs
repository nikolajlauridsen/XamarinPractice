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

        private double _weightRatio;
        public double WeightRatio { 
            get 
            {
                return _weightRatio;
            }
            set 
            {
                _weightRatio = value;
                NotifyPropertyChanged("WeightRatio");
            } 
        }
        private double _rollMin;
        public double RollMin {
            get 
            { return _rollMin; }
            set 
            {
                _rollMin = value;
                NotifyPropertyChanged("RollMin");
            } 
        }

        private double _rollMax;
        public double RollMax {
            get
            { return _rollMax; }
            set
            {
                _rollMax = value;
                NotifyPropertyChanged("RollMax");
            }
        }

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

        private double _springsMin;
        public double SpringsMin {
            get { return _springsMin; }
            set
            {
                _springsMin = value;
                NotifyPropertyChanged("SpringsMin");
            }
        }

        private double _springsMax;
        public double SpringsMax {
            get { return _springsMax; }
            set
            {
                _springsMax = value;
                NotifyPropertyChanged("SpringsMax");
            } 
        }
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

        public double CompFront {
            get 
            {
                return calcBase(CompMin, CompMax, WeightRatio);
            }
        }
        public double CompRear {
            get 
            {
                return calcBase(CompMin, CompMax, (1 - WeightRatio));
            }
        }
        public double ReboundFront { get { return CompFront * 0.6; } }
        public double ReboundRear { get { return CompRear * 0.6; } }

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
