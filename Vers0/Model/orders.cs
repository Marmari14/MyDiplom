//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vers0.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public orders()
        {
            this.products_in_order = new HashSet<products_in_order>();
        }
    
        public int number { get; set; }
        public string order_type { get; set; }
        public System.DateTime order_date { get; set; }
        public string contractor { get; set; }
        public Nullable<double> amount { get; set; }
        public string order_status { get; set; }
        public bool deleted { get; set; }
    
        public virtual contractor contractor1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<products_in_order> products_in_order { get; set; }
    }
}
