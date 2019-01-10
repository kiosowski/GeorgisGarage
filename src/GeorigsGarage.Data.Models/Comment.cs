using System;
using System.Collections.Generic;
using System.Text;

namespace GeorigsGarage.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            Replies = new HashSet<Reply>();
        }
        public string Id { get; set; }

        public string ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime PostedOn { get; set; }
    }
}
