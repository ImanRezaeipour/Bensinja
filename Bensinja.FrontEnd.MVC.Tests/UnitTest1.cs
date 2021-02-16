using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bestinja.Services;
using BestInja.Model;
using BestInja.Model.Vendor;
using FluentAssertions;
using Xunit;

namespace Bensinja.FrontEnd.MVC.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetCategory()
        {


            VendorService service = new VendorService();

            var t = await service.GetCategory();
            t.Should().NotBeNull();
            //var result =  await identity.Createuser(new CreateUserCommand()
            //   {
            //       UserName = "09123135143",
            //       Password = "123456"
            //   });
        }

        [Fact]
        public async Task GetImages()
        {


            ServiceImageService service = new ServiceImageService();

            var t = await service.ServiceImages(new Guid("98d47d29-b729-4a9f-8cb9-75735eb8a51f"));
            t.Should().NotBeNull();
    
        }

        [Fact]
        public async Task GetFacility()
        {



            VendorService service = new VendorService();

            var t = await service.GetFacilities();
            t.Should().NotBeNull();

        }

        [Fact]
        public async Task addVendor()
        {



            VendorService service = new VendorService();
            var model = new AddVendorModel()
            {
                Title = "test",
                UserId = Guid.NewGuid(),
                MainImage = "test.jpg",
                CategoryId = 10,
                Lng = 51.2063269,
                Lat = 35.6140752,
                PhoneNumber = "09123135143",
                Description = "توضیحات",
                Facilities = new List<int>() { 1, 2 }

            };
            await service.AddVendor(model);


        }

        [Fact]
        public async Task Get_vendor_by_id_should_not_be_null()
        {
            VendorService service = new VendorService();
            var id = new Guid("98d47d29-b729-4a9f-8cb9-75735eb8a51f");
            var vendor = await service.GetService(id);
            vendor.Should().NotBeNull();

        }
    }
}
