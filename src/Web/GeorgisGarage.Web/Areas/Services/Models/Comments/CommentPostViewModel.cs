using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Services.Models.Comments
{
    public class CommentPostViewModel
    {
        public string ServiceId { get; set; }

        [Display(Name = " ")]
        public int Rating { get; set; }

        public User Commentator { get; set; }

        [Required(ErrorMessage = "Моля попълнете полето.")]
        [StringLength(500, ErrorMessage = "Коментарът трябва да е между 10 и 500 символа.", MinimumLength = 10)]
        [Display(Name = "Коментар")]
        public string Comment { get; set; }
    }
}
