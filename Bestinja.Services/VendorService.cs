using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bestinja.Services.Contract;
using BestInja.Model.Vendor;
using Newtonsoft.Json;

namespace Bestinja.Services
{
    public class VendorService : IVendorService
    {

        public async Task AddVendor(AddVendorModel command)
        {
            try
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsJsonAsync("vendor", command);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("server error ");
                    }
                }



            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async void EditVendor(EditVendorModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PutAsJsonAsync("vendor", model);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("server error ");
                    }
                }



            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<List<CategoryQueryModel>> GetCategory()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("Category/GetAll");
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<List<CategoryQueryModel>>();
                    }
                    throw new Exception();

                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<List<FacilityModel>> GetFacilities()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("Facility/GetAll");
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<List<FacilityModel>>();
                    }
                    throw new Exception();

                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<EditVendorDto> GetService(Guid serviceId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:53345/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"VendorQuery/GetService/{serviceId}");

                    if (!response.IsSuccessStatusCode) throw new Exception("خطا در دریافت اطلاعات");
                    var contentResult = await response.Content.ReadAsAsync<EditVendorDto>();

                    return contentResult;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }

            }
        }
    }
}
