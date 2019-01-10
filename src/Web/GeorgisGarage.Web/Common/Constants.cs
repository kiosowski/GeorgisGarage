using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeorgisGarage.Web.Common
{
    public class Constants
    {
        public const string AdminRole = "Admin";
        public const string OwnerRole = "Owner";
        public static readonly string UserRole = "User";

        public const string AdminAndOwnerRoleAuth = "Admin,Owner";


        public static readonly string DateTimeFormat = "dd-MM-yyyy";

        public const string AdminArea = "Administration";
        public const string RoadsArea = "Roads";
        public const string ShopArea = "Shop";
    }
}
