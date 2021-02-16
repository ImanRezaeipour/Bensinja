using System.ComponentModel.DataAnnotations;

namespace BestInja.Model
{
    public class LoginViewModel
    {
        [RegularExpression("(\\+98|0)?9\\d{9}", ErrorMessage = "شماره وارد شده اشتباه است")]
        [Required(ErrorMessage = "لطفا نام کاریری را وارد نمایید")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور را وارد نمایید")]
        [MinLength(8, ErrorMessage = "رمز عبور اشتباه است")]
        public string Password { get; set; }

       
    }
}