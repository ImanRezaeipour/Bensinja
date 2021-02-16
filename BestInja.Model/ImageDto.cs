using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestInja.Model
{
    public class ImageDto
    {
      
        [Required(ErrorMessage = "لطفا عنوان را ارسال نمایید")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا آدرس را ارسال نمایید")]

        public string Url { get; set; }
        [Required(ErrorMessage = "لطفا اندازه را ارسال نمایید")]

        public long Size { get; set; }
        [Required(ErrorMessage = "لطفا آدرس دوم را ارسال نمایید")]

        public string ThumbUrl { get; set; }
        [Required(ErrorMessage = "لطفا شناسه سرویس را ارسال نمایید")]

        public Guid ServiceId { get; set; }

        public bool Status { get; set; }
        public int Id { get; set; }
    }
}
