using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaoTaoLaiXe.Models
{
    [MetadataType(typeof(DapAnCauHoiLietMetadata))]
    public partial class DapAnCauHoiLiet
    {
         
    }
    sealed class DapAnCauHoiLietMetadata
    {
        public int MaDapAn { get; set; }
        public int MaCauHoi { get; set; }
        public byte SoThuTu { get; set; }

        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        [Display(Name = "Đáp án đúng")]
        public bool DapAnDung { get; set; }

    }
}