using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Services.Models.Comments.Replies
{
    public class ReplyViewModel
    {
        public string Id { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
