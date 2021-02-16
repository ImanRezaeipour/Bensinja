using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bestinja.Services.Contract;
using BestInja.Model;
using BestInja.Model.Vendor;

namespace Bestinja.Services
{
  public  class ProfileService : IProfileService
    {
        public async Task<List<VendorListDto>> GetUserService(Guid userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53345/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"VendorQuery/GetUserService/{userId}");

                if (!response.IsSuccessStatusCode) throw new Exception("خطا در دریافت اطلاعات");
                var contentResult = await response.Content.ReadAsAsync<List<VendorListDto>>();
                return contentResult;

            }
        }


       
    }

    public class ServiceImageService : IServiceImageService
    {
        public async Task AddServiceImages(List<ImageDto> addImageDto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsJsonAsync("ServiceImage", addImageDto);
                    if (!response.IsSuccessStatusCode)
                    {
                       throw new Exception("خطا در ذخیره تصاویر");
                    }
                }

               


              
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<List<ImageDto>> ServiceImages(Guid id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"VendorQuery/GetServiceImages/{id}");
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت تصاویر");
                    }

                    return await response.Content.ReadAsAsync<List<ImageDto>>();
                }





            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task RemoveImageFromGallery(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"VendorQuery/Delete/{id}");
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت تصاویر");
                    }
                }





            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
