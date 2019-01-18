using System.Collections.Generic;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Web.Areas.Administration.Models.Users
{
    public class UsersServicesViewModelWrapper
    {
        public User User { get; set; }

        public ICollection<UsersServicesViewModel> UsersServicesViewModels { get; set; }
    }
}