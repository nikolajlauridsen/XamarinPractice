using System;
using System.ComponentModel;

namespace HPlusSports.Services
{
    public class Order : INotifyPropertyChanged
    {
        public Order()
        {
        }

        public string ProductName
        {
            get;
            set;
        }

        private int rate;

        public int Rating
        {
            get { return rate; }
            set
            {
                if (value != rate)
                {
                    rate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rating"));
                }
            }
        }

        private int qty;

        public int Quantity
        {
            get { return qty; }
            set{
                if(value != qty)
                {
                    qty = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Quantity"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
