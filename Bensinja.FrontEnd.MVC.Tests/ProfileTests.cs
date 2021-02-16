using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bestinja.Services;
using FluentAssertions;
using Xunit;

namespace Bensinja.FrontEnd.MVC.Tests
{
   public class ProfileTests
    {
        [Fact]

        public async Task Vendor_list_should_not_be_null()
        {
            ProfileService profileService = new ProfileService();
            UserIdentity identity = new UserIdentity();
            var user =await identity.GetUserByName("09384444636");
           var userVendors =await profileService.GetUserService(user.Id);
            userVendors.Should().NotBeNull();
        }
    }
}
