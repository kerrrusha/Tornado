using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace TornadoMVC.Models
{
    public class UserCredentials
    {
        [Key]
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
