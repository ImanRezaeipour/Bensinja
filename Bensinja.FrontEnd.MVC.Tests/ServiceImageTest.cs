using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Bestinja.Services;
using BestInja.Model;
using Xunit;
using Xunit.Sdk;

namespace Bensinja.FrontEnd.MVC.Tests
{
 public   class ServiceImageTest
    {
        [Fact]
        public async Task Add_service_images()
        {
            ServiceImageService service = new ServiceImageService();
            var model = new List<ImageDto>()
            {
                new ImageDto()
                {
                    Title = "image1",
                    Size = 232323,
                    ServiceId = new Guid("98D47D29-B729-4A9F-8CB9-75735EB8A51F"),
                    ThumbUrl = "sdf",
                    Url = "sdf"
                },
                new ImageDto()
                {
                    Title = "image2",
                    Size = 23232,
                    ServiceId = new Guid("98D47D29-B729-4A9F-8CB9-75735EB8A51F"),
                    ThumbUrl = "sdf",
                    Url = "sdf"
                }
            };
            await service.AddServiceImages(model);
        }
    }
}
