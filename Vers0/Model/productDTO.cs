using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Vers0.Model
{
    public class productDTO: INotifyPropertyChanged
    {
        private double _cost;
        private double _price;
        private int _count;
        public int number { get; set; }
        public int number_product { get; set; }
        public string name_product { get; set; }
        public string article_product { get; set;}
        public int balance_product { get; set; }

        public int count_product 
        { 
            get { return _count; }
            set
            {
                _count = value;
                cost_product = _count * _price;
                OnPropertyChanged("count_product");
                OnPropertyChanged("cost_product");
            }
        }
        public double price_product 
        {
            get { return _price; }
            set
            {
                _price = value;
                cost_product = _count * _price;
                OnPropertyChanged("price_product");
                OnPropertyChanged("cost_product");
            }
        }
        public double cost_product 
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged("cost_product");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
