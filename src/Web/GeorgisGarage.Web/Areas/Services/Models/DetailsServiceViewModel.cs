using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeorgisGarage.Web.Areas.Services.Models.Comments;
using GeorgisGarage.Web.Areas.Services.Models.Comments.Replies;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Services.Models
{
    public class DetailsServiceViewModel
    {
        public string Id { get; set; }

        public string ServiceName { get; set; }

        public string StartedTime { get; set; }

        public string EndTime { get; set; }

        public double ServiceTime { get; set; }

        public string Description { get; set; }

        public Image CoverPhoto { get; set; }

        public string Video { get; set; }

        public ICollection<Image> Images { get; set; }

        public User PostedBy { get; set; }

        public DateTime PostedOn { get; set; }

        public int SurfaceRating { get; set; }

        public int ViewRating { get; set; }

        public int PleasureRating { get; set; }

        //Comments Section

        public CommentPostViewModel Comment { get; set; }

        public ICollection<AllCommentsViewModel> CommentsViewModel { get; set; }

        public ReplyPostViewModel Reply { get; set; }



    }
}