using System.ComponentModel.DataAnnotations;

namespace OnlineFlightSearchAPI.Models
{
    public class UserLoginModel
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
