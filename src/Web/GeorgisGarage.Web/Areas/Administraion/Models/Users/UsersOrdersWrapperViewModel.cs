using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Administraion.Models.Users
{
    public class UsersOrdersWrapperViewModel
    {
        public User User { get; set; }

        public ICollection<UsersOrdersViewModel> UsersOrders { get; set; }
    }
}
