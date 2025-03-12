using System.Text.Json.Serialization;

namespace Relação1N.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }

        //relação
        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;
    }
}