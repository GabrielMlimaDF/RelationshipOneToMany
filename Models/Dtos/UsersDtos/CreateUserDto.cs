using System.ComponentModel.DataAnnotations;

namespace Relação1N.Models.Dtos.UsersDtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "O RoleId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Acesso deve ser válido.")]
        public int RoleId { get; set; }
    }
}