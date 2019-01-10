using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace GeorigsGarage.Data.Models
{
    public class Service
    {

        public Service()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Photos = new HashSet<Image>();
            this.Comments = new HashSet<Comment>();
        }
        public string Id { get; set; }

        public string ServiceName { get; set; }

        public string StartedTime { get; set; }

        public string EndTime { get; set; }

        public double ServiceTime { get; set; }

        public string CoverPhotoId { get; set; }
        public virtual CoverPhotoService CoverPhoto { get; set; }

        public virtual ICollection<Image> Photos { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public DateTime PostedOn { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}