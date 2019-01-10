using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeorgisGarage.Web.Areas.Services.Models.Comments.Replies;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Services.Models.Comments
{
    public class AllCommentsViewModel
    {
        public string Id { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public ICollection<ReplyViewModel> ReplyViewModel { get; set; }

        public DateTime PostedOn { get; set; }

        public bool IsDeleted { get; set; }

    }
}
