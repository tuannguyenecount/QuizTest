//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DaoTaoLaiXe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CauHoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CauHoi()
        {
            this.DapAns = new HashSet<DapAn>();
        }
    
        public int MaCauHoi { get; set; }
        public string NoiDung { get; set; }
        public Nullable<double> MaChuyenMuc { get; set; }
        public string Hinh { get; set; }
        public int MaCauHoiMoi { get; set; }
        public bool SuDung { get; set; }
        public bool CauHoiLiet { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DapAn> DapAns { get; set; }
        public virtual ChuyenMucCauHoi ChuyenMucCauHoi { get; set; }
    }
}
