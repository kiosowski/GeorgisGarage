using System;

namespace GeorigsGarage.Data.Models
{
    public class Image
    {
        public Image()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string PublicId { get; set; }

        public string ImageUrl { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }

        public string ServiceId { get; set; }
        public virtual Service Service { get; set; }

        //Posted by
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}