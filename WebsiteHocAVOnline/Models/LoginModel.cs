using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteHocAVOnline.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Tên Đăng NHập")]
        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Tên người dùng tối thiểu 4 ký tự và tối đa 20 ký tự")]
        [Required(ErrorMessage = "Xin hãy điền tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật Khẩu")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự và tối đa 20 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu của bạn")]
        public string MatKhau { get; set; }
    }
}