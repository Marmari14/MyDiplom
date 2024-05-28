using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vers0.Model
{
    public class ordersDTO
    {
        private DateTime _order_date;
        public int number { get; set; }
        public string type { get; set; }
        public DateTime order_date 
        {
            get => _order_date.Date;
            set 
            { 
                _order_date = value.Date;
            } 
        }
        public contractorsDTO contractor { get; set; }
        public Nullable<double> amount { get; set; }
        public string order_status { get; set; }
        public ordersDTO() 
        { 
            number = 0;
            order_date = DateTime.Now.Date;
            amount = 0;            
        }
    }
}
