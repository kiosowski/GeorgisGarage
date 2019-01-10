using System.ComponentModel.DataAnnotations;

namespace GeorgisGarage.Web.Areas.Shop.Models
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        [Display(Name = "Име")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Display(Name = "Град")]
        public string City { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Допълнителна информация")]
        public string AdditionalInformation { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Phone(ErrorMessage = "Телефонният номер не е валиден")]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

    }
}
