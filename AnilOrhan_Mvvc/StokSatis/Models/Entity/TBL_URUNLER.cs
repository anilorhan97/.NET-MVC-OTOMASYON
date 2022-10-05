//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StokSatis.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TBL_URUNLER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_URUNLER()
        {
            this.TBL_SATISLAR = new HashSet<TBL_SATISLAR>();
        }
    
        public int URUNID { get; set; }
        [Required(ErrorMessage = "�r�n ad� bo� ge�ilemez!")]
        public string URUNAD { get; set; }
        [Required(ErrorMessage = "�r�n markas� bo� ge�ilemez!")]
        public string MARKA { get; set; }
        public Nullable<int> URUNKATEGORI { get; set; }
        public Nullable<int> URUNALAN { get; set; }
        [Required(ErrorMessage = "�r�n stok say�s� bo� ge�ilemez!")]
        public Nullable<int> STOK { get; set; }
        [Required(ErrorMessage = "�r�n fiyat� bo� ge�ilemez!")]
        public Nullable<decimal> URUNFIYAT { get; set; }
        public string RESIMM { get; set; }
    
        public virtual TBL_ALANLAR TBL_ALANLAR { get; set; }
        public virtual TBL_KATEGORILER TBL_KATEGORILER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_SATISLAR> TBL_SATISLAR { get; set; }
    }
}