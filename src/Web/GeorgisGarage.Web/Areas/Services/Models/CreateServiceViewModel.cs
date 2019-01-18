using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GeorgisGarage.Web.Areas.Services.Models
{
    public class CreateServiceViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Добави име")]
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
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid service time.")]
        public double ServiceTime { get; set; }

        //public virtual ICollection<Image> Photos { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Информация за услугата")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Url]
        [Display(Name = "Видео")]
        public string Video { get; set; }

        [Required(ErrorMessage = "Добави снимка")]
        [Display(Name = "Основна снимка(Провлечи и пусни снимката тук)")]
        public IFormFile CoverPhoto { get; set; }


        [Display(Name = "Снимки(Провлечи и пусни снимката тук)")]
        [DataType(DataType.Upload)]
        public ICollection<IFormFile> Images { get; set; }

        public DateTime PostedOn { get; set; }


        [Display(Name = "")]
        public int Material { get; set; }

        [Display(Name = "")]
        public int Performance { get; set; }

        [Display(Name = "")]
        public int Time { get; set; }
    }
}
