using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GeorigsGarage.Data.Models
{
    // Add profile data for application users by adding properties to the GeorgisGarageUser class
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Services = new HashSet<Service>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }

    }
}
