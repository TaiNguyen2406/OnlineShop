using System.ComponentModel;
using System.Web.Mvc;

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
  
    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name="Tài khoản")]
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [StringLength(32)]
        [Display(Name = "Mật khẩu")]
        [Range(5, 32, ErrorMessage = "Mật khẩu phải từ 5 đến 32 ký tự")]
        public string Password { get; set; }

        [StringLength(20)]
        [Display(Name = "Nhóm người dùng")]
        public string GroupID { get; set; }

        [StringLength(500)]
        [Display(Name = "Họ tên")]
        /// tự tạo bộ lọc cho mình
        [FormValidation.CustomAttribute.MinCharAttribute(5)]
        public string Name { get; set; }

        [StringLength(5000)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(500)]
        [Remote("IsEmailExist", "User", ErrorMessage = "Email Already Exist. Please choose another email.")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{4})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number")]
        [Display(Name = "SĐT")]
        public string Phone { get; set; }

        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
