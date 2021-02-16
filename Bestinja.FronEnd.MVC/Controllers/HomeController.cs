using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Bestinja.Framework.Logging;
using Bestinja.Services;
using Bestinja.Services.Contract;
using BestInja.Model;
using BestInja.Model.UserManagment;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;



namespace Bestinja.FronEnd.MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserIdentity _userIdentity;
        private readonly ICusotmLogger _cusotmLogger;

        public HomeController(IUserIdentity userIdentity, ICusotmLogger cusotmLogger)
        {
            _userIdentity = userIdentity;
            _cusotmLogger = cusotmLogger;
        }
        public ActionResult Index()
        {
            _cusotmLogger.Debug("sdfsdf",new Exception());
            _cusotmLogger.Warn("sdfsdf",new Exception());
            _cusotmLogger.Error(new Exception("sdfsdfsdfsdf"));
            return View();
        }
        //todo authenticate user
        public async Task<ActionResult> Autenticate(string userName)
        {
            var user = await _userIdentity.GetUserByName(userName);
            UserAutenticate(user);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private void UserAutenticate(ApplicationUserDto applicationUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Id.ToString()),
                new Claim(ClaimTypes.Name, applicationUser.UserName),
                new Claim("userState", applicationUser.UserName)
            };

            // create required claims

            // custom – my serialized AppUserState object

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7),
            }, identity);
        }

        public ActionResult IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);

            return RedirectToAction("Index");
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public async Task<ActionResult> SendCodeAgain(string userName)
        {
            await _userIdentity.SendCodeAgain(userName);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ConfirmCode(string userName, int code)
        {
            return Json(_userIdentity.CheckConfirmCode(code, userName), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CheckExistUser(string userName)
        {
            var result = await _userIdentity.GetUserByName(userName);
            if (result.UserName != null)
                await _userIdentity.SendCodeAgain(userName);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                bool isValid = false;
                if (!ModelState.IsValid)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                var result = await _userIdentity.LoginUser(model);
                if (result.UserName != null)
                {
                    UserAutenticate(result);
                    isValid = true;
                }

                return Json(isValid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(500, "خطایی سمت سرور روی داده"); // Bad Request


            }

        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(ReturnError(), JsonRequestBehavior.AllowGet);

                var userLogin = new CreateUserCommand(registerModel.UserName, registerModel.Password);
                var result = await _userIdentity.Createuser(userLogin);

                var jsonResut = new
                {
                    userName = registerModel.UserName,
                    ConfirmCode = result
                };

                await Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    return Json(jsonResut, JsonRequestBehavior.AllowGet);
                });

                return Json(jsonResut, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }



        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                    return Json(ReturnError(), JsonRequestBehavior.AllowGet);

                var result = await _userIdentity.ChangePassword(model);
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }
        public List<string> ReturnError()
        {
            var errors = ModelState.Values.SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return errors;
        }


    }

}