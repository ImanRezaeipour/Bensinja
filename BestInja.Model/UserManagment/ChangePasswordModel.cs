using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestInja.Model
{
    public class ChangePasswordModel
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور را وارد نمایید")]
        [MinLength(8, ErrorMessage = "رمز عبور حداقل باید 8 رقم باشد")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا تکرار رمز عبور را وارد نمایید")]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور اشتباه است")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}