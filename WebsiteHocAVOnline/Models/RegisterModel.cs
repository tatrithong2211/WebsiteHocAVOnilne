using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteHocAVOnline.Models
{
    public class RegisterModel
    {
        [Key]
        public int IDTaiKhoan { get; set; }

        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng của bạn")]
        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Tên người dùng tối thiểu 4 ký tự và tối đa 20 ký tự")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật Khẩu")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự và tối đa 20 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu của bạn")]
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không đúng")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu của bạn")]
        public string ConfirmMatKhau { get; set; }

        [Display(Name = "Họ và tên")]
        [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "Mật khẩu tối thiểu 10 ký tự và tối đa 50 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên đầy đủ của bạn")]
        public string Hoten { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email phải có @ gần cuối")]
        [Required(ErrorMessage = "Vui lòng nhập email của bạn")]
        public string Email { get; set; }

        [Display(Name = "Địa Chỉ")]
        [StringLength(maximumLength: 255, ErrorMessage = "Địa chỉ tối đa 255 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ của bạn")]
        public string DiaChi { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Xin vui lòng nhập một số điện thoại hợp lệ")]
        [Required(ErrorMessage = "Xin vui lòng điền số điện thoại của bạn")]
        public Nullable<int> SDT { get; set; }
    }
}