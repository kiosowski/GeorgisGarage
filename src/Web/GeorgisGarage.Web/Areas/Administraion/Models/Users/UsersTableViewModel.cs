namespace GeorgisGarage.Web.Areas.Administraion.Models.Users
{
    public class UsersTableViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int Services { get; set; }

        public string Role { get; set; }
    }
}