using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Bestinja.Services;
using Bestinja.Services.Contract;
using BestInja.Model;
using Microsoft.AspNet.Identity;
using Westwind.Web.Mvc;

namespace Bestinja.FronEnd.MVC.Controllers
{
    //[Authorize]
    public class ProfilesController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IServiceImageService _imageService;

        public ProfilesController()
        {
            _profileService = new ProfileService();
            _imageService = new ServiceImageService();
        }

        // GET: Profile
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();

            var services = await _profileService.GetUserService(new Guid(userId));

            return View(services);
        }

        public async Task<ActionResult> _Gallery(Guid id)
        {
            var serviceImages = await _imageService.ServiceImages(id);
            ViewBag.ServiceId = id;
            return View(serviceImages);
        }
        [HttpPost]
        public ActionResult Upload()
        {

            if (Request.Files.Count > 0)
            {

                FileUpload(Request.Files);
            }
            return null;
        }

        public ActionResult UploadToServer(Guid serviceId)
        {
            try
            {


                List<ImageDto> addImageDtos = new List<ImageDto>();
                var tempFolder = $"~/Temporary/{User.Identity.GetUserId()}";
                var destinationFolder = $"~/Images/vendor/{User.Identity.GetUserId()}/org";
                var destinationFolderthumb = $"~/Images/vendor/{User.Identity.GetUserId()}/thumb";

                if (!Directory.Exists(Server.MapPath(destinationFolder)))
                {
                    Directory.CreateDirectory(Server.MapPath(destinationFolder));
                    Directory.CreateDirectory(Server.MapPath(destinationFolderthumb));

                }


                var sourceFiles = Directory.GetFiles(Server.MapPath(tempFolder));
                foreach (string file in sourceFiles)
                {
                    FileInfo mFile = new FileInfo(file);
                    // to remove name collisions
                    if (new FileInfo(Server.MapPath(destinationFolder) + "\\" + mFile.Name).Exists == false)
                    {

                        mFile.MoveTo(Server.MapPath(destinationFolder) + "\\" + mFile.Name);


                    }
                    Size.ResizeImage(Server.MapPath(destinationFolder) + "\\" + mFile.Name,
                                            Server.MapPath(destinationFolderthumb + "\\" + mFile.Name),
                                            ImageFormat.Jpeg, 150, 200);
                    addImageDtos.Add(new ImageDto()
                    {
                        Url = $"{destinationFolder}/{mFile.Name}",
                        ThumbUrl = $"{destinationFolderthumb}/{mFile.Name}",
                        Size = mFile.Length,
                        Title = mFile.Name,
                        ServiceId = serviceId
                    });
                }

                _imageService.AddServiceImages(addImageDtos);
                Directory.Delete(Server.MapPath(tempFolder), true);


                return Json(1, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(0, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult DeleteImage(int id)
        {
            _imageService.RemoveImageFromGallery(id);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteFile(string fileName)
        {
            string root = "~/Temporary/";

            string path = $"{root}{ User.Identity.GetUserId()}/{fileName}";
            var url = Path.Combine(Server.MapPath(path));

            if (System.IO.File.Exists(url))
                System.IO.File.Delete(url);

            return null;
        }

        private List<string> FileUpload(HttpFileCollectionBase mainImage)
        {
            List<string> addressList = new List<string>();
            string root = "~/Temporary/";

            string path = $"{root}{ User.Identity.GetUserId()}/";
            string directory = $"{root}{User.Identity.GetUserId()}";

            Directory.CreateDirectory(Server.MapPath(directory));
            SaveFileToDirectory(mainImage, addressList, path);

            return addressList;


        }



        private void SaveFileToDirectory(HttpFileCollectionBase mainImage, List<string> addressList, string path)
        {
            for (int i = 0; i < mainImage.Count; i++)
            {
                var image = Image.FromStream(mainImage[i].InputStream, true, true);
                string trailingPath = Path.GetFileName(mainImage[i]?.FileName);
                string fullPath = Path.Combine(Server.MapPath(path), trailingPath);
                CompressImage(image, ImageFormat.Jpeg, 750, 550, fullPath);

                addressList.Add($"{path}{ mainImage[i]?.FileName}");

            }
        }

        private void CompressImage(Image img, ImageFormat format, int width, int height, string address)
        {
            Image thumbNail = new Bitmap(width, height, img.PixelFormat);
            Graphics g = Graphics.FromImage(thumbNail);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rect = new Rectangle(0, 0, width, height);
            g.DrawImage(img, rect);
            thumbNail.Save(address, format);
        }

        private void CheckAndCreateDirectory(string directory)
        {


        }
    }


    public class Size
    {
        public static bool ResizeImage(string orgFile, string resizedFile, ImageFormat format, int width, int height)
        {
            try
            {

                using (Image img = Image.FromFile(orgFile))
                {
                    Image thumbNail = new Bitmap(width, height, img.PixelFormat);
                    Graphics g = Graphics.FromImage(thumbNail);
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle rect = new Rectangle(0, 0, width, height);
                    g.DrawImage(img, rect);
                    thumbNail.Save(resizedFile, format);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}