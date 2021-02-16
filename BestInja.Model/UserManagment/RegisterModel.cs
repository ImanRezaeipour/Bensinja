using System.ComponentModel.DataAnnotations;

namespace BestInja.Model
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "لطفا نام کاریری را وارد نمایید")]
        [RegularExpression("(\\+98|0)?9\\d{9}", ErrorMessage = "شماره وارد شده اشتباه است")]

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور را وارد نمایید")]
        [MinLength(8, ErrorMessage = "رمز عبور حداقل باید 8 رقم باشد")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا تکرار رمز عبور را وارد نمایید")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "تکرار رمز عبور اشتباه است")]
        public string ConfirmPassword { get; set; }
    }
}