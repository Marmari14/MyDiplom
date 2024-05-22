using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vers0
{
    public class AppProperties
    {
        public  List<string> orderStatus { get; } = new List<string> { "создан", "подтвержден", "собран", "отгружен", "доставлен", "отменен" };
        public  List<string> orderType { get; }  = new List<string> { "Заказ_поставщику", "Заказ_покупателя", "Поступление" };
        public  List<string> contractorType { get; } = new List<string> { "Юридическое лицо", "Физическое лицо", "Индивидуальный предприниматель"};
        
        public  List<string> unit { get; }  = new List<string> { "шт", "уп", "л" };
        public readonly List<string> seasons = new List<string> { "Зима", "Весна", "Лето", "Осень" } ;
        public readonly int expiration_date = 2;

    }
}
