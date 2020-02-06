using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaoTaoLaiXe.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}