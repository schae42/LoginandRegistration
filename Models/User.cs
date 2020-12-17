using System;
using System.ComponentModel.DataAnnotations;

namespace LoginAndRegistration.Models
{
    public class User
    {
        [Display(Name="First Name")]
        [Required]
        [MinLength(2)]
        public string first_name {get; set;}

        [Display(Name="Last Name")]
        [Required]
        [MinLength(2)]
        public string last_name {get; set;}

        [Display(Name="EMail ID")]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage="Invalid Email Address")]
        public string email {get; set;}

        [Display(Name="Password")]
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password {get; set;}

        [Display(Name="Confirm Password")]
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string confirmpwd {get; set;}
    }

    public class Login
    {
        [Display(Name="EMail ID")]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage="Invalid Email Address")]
        public string email {get; set;}

        [Display(Name="Password")]
        [Required]
        [DataType(DataType.Password)]
        public string password {get; set;}
    }

    public class HomeContent
    {
        public User RegisterUser {get; set;}
        public Login LoginUser {get; set;}
    }
}