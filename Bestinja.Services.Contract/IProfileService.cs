using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestInja.Model.Vendor;

namespace Bestinja.Services.Contract
{
    public interface IProfileService
    {
        Task<List<VendorListDto>> GetUserService(Guid userId);
    }
}