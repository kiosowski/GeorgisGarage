using System;
using System.Collections.Generic;
using System.Text;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Services.Contracts
{
    public interface IServicesIndexService
    {
        ICollection<Service> GetAllServices();
        ICollection<Service> GetLatestServices();
        ICollection<Service> GetLongestServices();
        ICollection<Service> GetTopServices();
        ICollection<Service> GetCurrentUserServicesById(string id);
    }
}
