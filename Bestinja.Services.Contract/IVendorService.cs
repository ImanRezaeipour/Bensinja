using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestInja.Model.Vendor;

namespace Bestinja.Services.Contract
{
    public interface IVendorService
    {
        Task AddVendor(AddVendorModel model);
        void EditVendor(EditVendorModel model);
        Task<List<CategoryQueryModel>> GetCategory();
        Task<List<FacilityModel>> GetFacilities();
        Task<EditVendorDto> GetService(Guid serviceId);
    }
}