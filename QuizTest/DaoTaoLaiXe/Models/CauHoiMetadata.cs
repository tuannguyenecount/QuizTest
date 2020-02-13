using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaoTaoLaiXe.Models
{
    [MetadataType(typeof(CauHoiMetadata))]
    public partial class CauHoi
    {
    }
    sealed class CauHoiMetadata
    {
        [Display(Name = "Mã câu hỏi")]
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        public int MaCauHoi { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        public string NoiDung { get; set; }

        [Display(Name = "Chuyên mục")]
        public string MaChuyenMuc { get; set; }

        [Display(Name = "Hình")]
        public string Hinh { get; set; }
    }
}