using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BestInja.Model.Vendor
{

   
    public  class AddVendorModel
    {

        [Required(ErrorMessage = "شناسه کاربر ارسال نگردیده")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "تصویر اصلی ارسال نگردیده")]

        public string MainImage { get; set; }
 
        [Required(ErrorMessage = "عنوان سرویس ارسال نگردیده")]

        public string Title { get; set; }
        [Required(ErrorMessage = "عرض جغرافیایی ارسال نگردیده")]

        public double Lat { get; set; }
        [Required(ErrorMessage = "طول جغرافیای ارسال نگردیده")]

        public double Lng { get; set; }
        [Required(ErrorMessage = "دسته بندی ارسال نگردیده")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "لطفا تلفن ثابت را ارسال نمایید")]
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public List<int> Facilities { get; set; }


    }

    public class EditVendorDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public string MainImage { get; set; }
        public ServiceState ServiceState { get; set; }

        public string Description { get; set; }
        public ServiceGeoDto ServiceGeography { get; set; }
        public string PhoneNumber { get; set; }
        public List<FacilityDto> Facility { get; set; }
        public CategoryDto Category { get; set; }
        public int CategoryId { get; set; }
        
     

    }

    public class ServiceGeoDto
    {

        public string Address { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string LocationType { get; set; }
        public string FormattedAddress { get; set; }
    }

    public class FacilityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FontIcon { get; set; }
    }
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
