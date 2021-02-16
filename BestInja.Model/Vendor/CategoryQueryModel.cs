using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestInja.Model.Vendor
{
    public class CategoryQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public int? ParentId { get; set; }
        public List<CategoryQueryModel> Childrens { get; set; }
    }

    public class VendorListDto
    {
        public string Title { get; set; }
        public Guid Id { get; set; }
        public string MainImage { get; set; }
        public ServiceState ServiceState { get; set; }
    }


    public enum ServiceState
    {
        Active = 1,
        Waiting = 2,
        Blocked = 3
    }


}