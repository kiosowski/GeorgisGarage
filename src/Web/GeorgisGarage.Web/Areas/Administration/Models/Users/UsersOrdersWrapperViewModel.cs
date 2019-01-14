using System.Collections.Generic;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Administration.Models.Users
{
    public class UsersOrdersWrapperViewModel
    {
        public User User { get; set; }

        public ICollection<UsersOrdersViewModel> UsersOrders { get; set; }
    }
}
