//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace haber.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Haber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Haber()
        {
            this.Resim = new HashSet<Resim>();
            this.Yorum = new HashSet<Yorum>();
        }
    
        public int HaberId { get; set; }
        public string HaberBaslik { get; set; }
        public Nullable<int> Kategori { get; set; }
        public string HaberIcerik { get; set; }
        public Nullable<int> Yazar { get; set; }
        public string YayinlanmaTarihi { get; set; }
    
        public virtual Kategori Kategori1 { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resim> Resim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
