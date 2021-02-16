using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestInja.Model;

namespace Bestinja.Services.Contract
{
    public interface IServiceImageService
    {
        Task AddServiceImages(List<ImageDto> addImageDto);
        Task<List<ImageDto>> ServiceImages(Guid id);

        Task RemoveImageFromGallery(int id);
    }
}
