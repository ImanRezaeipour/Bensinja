using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bestinja.FronEnd.MVC.Controllers;
using Bestinja.Services;
using BestInja.Model;
using FluentAssertions;
using Xunit;

namespace Bensinja.FrontEnd.MVC.Tests
{
   public class UserManagementTest
   {
       private UserIdentity _userIdentity;

       public UserManagementTest()
       {
           _userIdentity = new UserIdentity();
       }
        [Fact]

        public async Task GetUserByName()
        {
            var user =await _userIdentity.GetUserByName("09384444636");
            user.Id.Should().NotBe(Guid.Empty);
            user.UserName.Should().Be("09384444636");


        }
        [Fact]
       public async Task User_should_be_autenticate()
       {
           var userLoign = new LoginViewModel()
           {
               UserName = "09384444636",
               Password = "11111111"
           };
       }
    }
}
