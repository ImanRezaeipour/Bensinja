using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestInja.Model.Vendor
{
    public class EditVendorModel
    {

        [Required(ErrorMessage = "شناسه کاربر ارسال نشده")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "لطفا تصاویر را ارسال نمایید")]
        public List<string> Images { get; set; }
        [Required(ErrorMessage = "لطفا امکانات را ارسال نمایید")]
        public List<int> Facility { get; set; }
        [Required(ErrorMessage = "لطفا عنوان را ارسال نمایید")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا عرض جغرافیایی را ارسال نمایید")]
        public double Lat { get; set; }
        [Required(ErrorMessage = "لطفا طول جغرافیایی را ارسال نمایید")]
        public double Lng { get; set; }
        [Required(ErrorMessage = "لطفا دسته بندی را ارسال نمایید")]
        public int CategoryId { get; set; }

        public List<FacilityModel> FacilityDtos { get; set; }

    }
}