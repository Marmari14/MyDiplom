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
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.products_in_order = new HashSet<products_in_order>();
        }
    
        public int number { get; set; }
        public string article { get; set; }
        public string name_product { get; set; }
        public string unit_of_measurement { get; set; }
        public decimal sale_price { get; set; }
        public int minimum_balance { get; set; }
        public int count { get; set; }
        public string description_product { get; set; }
        public Nullable<int> Shelf_life { get; set; }
        public string season { get; set; }
        public bool deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<products_in_order> products_in_order { get; set; }
    }
}
