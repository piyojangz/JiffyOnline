using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace jiffyOnline.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string AUTH_USER { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string AUTH_PASS { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        public int ID { get; set; }
        public Nullable<int> CUSTOMER_GROUP { get; set; }
        [Required]
        public string FNAME { get; set; }
        [Required]
        public string LNAME { get; set; }
        [Required]
        public string EMAIL { get; set; }
        public System.DateTime DATE_OF_BIRTH { get; set; }
        public bool SEND_NEWS { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string AUTH_USER { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "AUTH_PASS")]
        public string AUTH_PASS { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "REAUTH_PASS")]
        [Required]
        [Compare("AUTH_PASS", ErrorMessage = "The password and confirmation password do not match.")]
        public string REAUTH_PASS { get; set; }
        public string TYPE { get; set; } //?
        [Required]
        public string COMPANYNAME { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string ADDRESS4 { get; set; }
        [Required]
        public int PROVINCE_ID { get; set; }
        [Required]
        public int DISTRICT_ID { get; set; }
        [Required]
        public int AMPHUR_ID { get; set; }
        [Required]
        public string POSTCODE { get; set; }
        [Required]
        public string TEL { get; set; }
        [Required]
        public string MOBILE { get; set; }
        [Required]
        public string FAX { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public Nullable<System.DateTime> DELETE_DATE { get; set; }
        public string DELETE_BY { get; set; }
        public string GENDER { get; set; }
        public Nullable<int> POINT { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
