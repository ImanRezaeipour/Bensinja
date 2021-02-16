using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bestinja.Services.Contract;
using BestInja.Model;
using BestInja.Model.UserManagment;
using Newtonsoft.Json;

namespace Bestinja.Services
{
    public class UserIdentity : IUserIdentity, IDisposable
    {

        private static Dictionary<string, int> confirmCodeDictionary = new Dictionary<string, int>();
        
        public bool CheckConfirmCode(int confirmCode, string userName)
        {
            var userCodes = confirmCodeDictionary.FirstOrDefault(x => x.Key == userName && x.Value == confirmCode);
            if (userCodes.Key == null) return false;
            confirmCodeDictionary.Remove(userCodes.Key);
            return true;

        }

        public async Task SendCodeAgain(string userName)
        {
            var userCodes = confirmCodeDictionary.FirstOrDefault(x => x.Key == userName);
            if (userCodes.Key == null)
            await SendMessage(userName, GenerateRandomNumber(new CreateUserCommand(userName, null)));

        }

        public async Task<ApplicationUserDto> GetUserByName(string userName)
        {
            try
            {
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"UserManagment/GetUserByName/{userName}");
                    if (!response.IsSuccessStatusCode) return new ApplicationUserDto();
                    var user = await response.Content.ReadAsAsync<ApplicationUserDto>();
                    return user;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> Createuser(CreateUserCommand command)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsJsonAsync("UserManagment", command);
                    if (response.IsSuccessStatusCode)
                    {
                        var userCodes = confirmCodeDictionary.Where(x => x.Key == command.UserName).ToList();
                        foreach (var keyValuePair in userCodes)
                        {
                            confirmCodeDictionary.Remove(keyValuePair.Key);
                        }
                        var randomNumber = GenerateRandomNumber(command);
                       await SendMessage(command.UserName, randomNumber);
                        return randomNumber;
                    }
                }




                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

        }

        private static int GenerateRandomNumber(CreateUserCommand command)
        {
            var randomNumber = new Random().Next(11111, 99999);
            confirmCodeDictionary.Add(command.UserName, randomNumber);
            return randomNumber;
        }

        public async Task<ApplicationUserDto> LoginUser(LoginViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsJsonAsync("UserManagment/ValidationUser", model);
                    if (response.IsSuccessStatusCode)
                    {
                       var user = await response.Content.ReadAsAsync<ApplicationUserDto>();
                        return user;
                    }
                }


                throw new Exception("خطای ورود");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message,e);
            }
         
        }


        public bool ForgetPassword(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangePassword(ChangePasswordModel newPassword)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsJsonAsync("UserManagment/ChangePassword", newPassword);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message,e);
            }
           
        }

        public void Dispose()
        {

        }

        private async Task SendMessage(string mobileNumber, int code)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://rest.payamak-panel.com");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                //client.DefaultRequestHeaders.Add("content-type", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Add("postman-token", "fcddb5f4-dc58-c7d5-4bf9-9748710f8789");
                client.DefaultRequestHeaders.Add("cache-control", "no-cache");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/SendSMS/SendSMS", new SmsParameter
                {
                    From = "50001060657928",
                    To = mobileNumber,
                    Password = "7975",
                    Text = code,
                    UserName = "09351616724"
                });

                if (response.IsSuccessStatusCode)
                {


                }
            };
        }

    }
    public class SmsParameter
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Password { get; set; }
        public object Text { get; set; }
        public string UserName { get; set; }
    }

}
