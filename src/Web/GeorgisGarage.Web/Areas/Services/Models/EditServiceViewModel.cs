using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GeorigsGarage.Data.Models;
using Microsoft.AspNetCore.Http;

namespace GeorgisGarage.Web.Areas.Services.Models
{
    public class EditServiceViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Име на услугата")]
        [DataType(DataType.Text)]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Начален час")]
        [DataType(DataType.Text)]
        public string StartedTime { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Час на приключване")]
        [DataType(DataType.Text)]
        public string EndTime { get; set; }

        [Display(Name = "Времетраене")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid road length.")]
        public double ServiceTime { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Информация за услугата")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Url] [Display(Name = "Видео")] public string Video { get; set; }

        [Display(Name = "Основна снимка(Провлечи и пусни снимката тук)")]
        public IFormFile CoverPhoto { get; set; }

        [Display(Name = "Снимки(Провлечи и пусни снимките тук)")]
        [DataType(DataType.Upload)]
        public ICollection<IFormFile> NewImages { get; set; }

        public ICollection<Image> Images { get; set; }

        public DateTime PostedOn { get; set; }

        [Display(Name = "")] public int ViewRating { get; set; }

        [Display(Name = "")] public int SurfaceRating { get; set; }

        [Display(Name = "")] public int PleasureRating { get; set; }
    }
}