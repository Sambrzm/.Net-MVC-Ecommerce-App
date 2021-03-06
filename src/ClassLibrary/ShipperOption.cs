//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShipperOption
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShipperOption()
        {
            this.Invoiceds = new HashSet<Invoiced>();
        }
    
        public int ShippingID { get; set; }
        public int ShipperID { get; set; }
        public int LocationID { get; set; }
        public int MethodID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoiced> Invoiceds { get; set; }
        public virtual Shipper Shipper { get; set; }
        public virtual ShippingLocation ShippingLocation { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
    }
}
