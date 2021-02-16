using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestInja.Model;
using BestInja.Model.UserManagment;

namespace Bestinja.Services.Contract
{
    public interface IUserIdentity
    {
        Task<int> Createuser(CreateUserCommand model);
        Task<ApplicationUserDto> LoginUser(LoginViewModel model);
        bool ForgetPassword(string userName);
        Task<bool> ChangePassword(ChangePasswordModel newPassword);
        bool CheckConfirmCode(int code,string userName);
        Task SendCodeAgain(string userName);
        Task<ApplicationUserDto> GetUserByName(string userName);
    }
}
