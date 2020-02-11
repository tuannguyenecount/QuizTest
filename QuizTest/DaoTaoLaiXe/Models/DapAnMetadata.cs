using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaoTaoLaiXe.Models
{
    [MetadataType(typeof(DapAnMetadata))]
    public partial class DapAn
    {
    }
    sealed class DapAnMetadata
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