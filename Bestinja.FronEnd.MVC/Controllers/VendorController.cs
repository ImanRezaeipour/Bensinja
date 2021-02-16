using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bestinja.Services;
using Bestinja.Services.Contract;
using BestInja.Model.Vendor;
using Westwind.Web.Mvc;

namespace Bestinja.FronEnd.MVC.Controllers
{
    [Authorize]
    public class VendorController : Controller
    {
        private readonly IVendorService _vendorService;

        public VendorController()
        {
            _vendorService = new VendorService();
        }
        // GET: Vendor
        public ActionResult AddVendor()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AddVendor(AddVendorModel model, HttpPostedFileBase mainImage)
        {

            if (!ModelState.IsValid)
                return View(model);

            model.MainImage = FileUpload(model.Title, "vendor", mainImage);
            await _vendorService.AddVendor(model);
            return RedirectToAction("Index","Profiles");
        }

        public async Task<ActionResult> EditVendor(Guid vendorId)
        {
            var vendor =await _vendorService.GetService(vendorId);
            ViewBag.Checked = vendor.Facility.Select(x => x.Id).ToList();
            ViewBag.Facilites =await _vendorService.GetFacilities();
            return View(vendor);
        }
        public ActionResult MapTest()
        {
            return View();
        }

        public async Task<ActionResult> _Facility()
        {

            var facilities = await _vendorService.GetFacilities();

            string postsHtml = ViewRenderer.RenderPartialView("~/views/vendor/_Facility.cshtml", facilities);

            return Json(postsHtml, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetCategroy()
        {

            var category = await _vendorService.GetCategory();
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FormLoad(int categroyId)
        {
            string postsHtml = ViewRenderer.RenderPartialView("~/views/vendor/_RegisterBody.cshtml");
            return Json(postsHtml, JsonRequestBehavior.AllowGet);

        }

        public ActionResult LoadScript()
        {
            string scritps = ViewRenderer.RenderPartialView("~/views/vendor/_RegisterBodyScript.cshtml");

            return Json(scritps, JsonRequestBehavior.AllowGet);

        }

            private string FileUpload(string title, string serviceType, HttpPostedFileBase mainImage)
            {

                string path = $"~/Images/{serviceType}/" + title + "/";
                string directory = $"~/Images/{serviceType}/" + title;

                if (mainImage != null)
                {

                    Directory.CreateDirectory(Server.MapPath(directory));
                    mainImage.SaveAs(Path.Combine(Server.MapPath(path), DateTime.Now.ToString("yyyy_MM_dd_mm_ss") + "_" + mainImage.FileName));
                    return $"{path}{mainImage.FileName}";
                }

                return null;
            }

    }
}