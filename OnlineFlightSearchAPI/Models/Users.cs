using System.ComponentModel.DataAnnotations;

namespace OnlineFlightSearchAPI.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
