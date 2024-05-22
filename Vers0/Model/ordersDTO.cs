using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vers0.Model
{
    public class ordersDTO
    {
        public int number { get; set; }
        public string type { get; set; }
        public System.DateTime order_date { get; set; }
        public contractorsDTO contractor { get; set; }
        public Nullable<double> amount { get; set; }
        public string order_status { get; set; }

        public ordersDTO() 
        { 
            number = 0;
            order_date = DateTime.Now;
            amount = 0;
        }
    }
}
