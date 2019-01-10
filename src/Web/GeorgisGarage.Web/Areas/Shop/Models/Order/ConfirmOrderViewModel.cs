using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeorgisGarage.Web.Areas.Shop.Models.Order
{
    public class ConfirmOrderViewModel
    {
        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string AdditionalInformation { get; set; }

    }
}
