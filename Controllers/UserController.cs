using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relação1N.Data;
using Relação1N.Models;
using Relação1N.Models.Dtos.UsersDtos;

namespace Relação1N.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ContextApp _contextApp;

        public UserController(ContextApp contextApp)
        {
            _contextApp = contextApp;
        }

        [HttpGet("/v1/user")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var users = await _contextApp.Users
            .Include(x => x.Role)  // Apenas incluindo a Role
            .AsNoTracking()
            .Select(user => new
            {
                user.Id,
                user.Name,
                Acesso = user.Role.Name // Pegando apenas o nome da Role
            })
            .ToListAsync();

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "Códido de erro: 02X01 - Erro ao buscar usuários!");
            }
        }

        [HttpPost("/v1/user")]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var role = await _contextApp.Roles.FindAsync(model.RoleId);
            if (role == null)
                return NotFound("Nivel de acesso não encontrada!");
            bool emailVerify = await _contextApp.Users.AnyAsync(x => x.Email == model.Email);
            if (emailVerify)
                return StatusCode(500, "Tente recuperar a senha ou use um email diferente!");
            try
            {
                var newUser = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    RoleId = model.RoleId,
                    CreateAt = DateTime.UtcNow
                };
                _contextApp.Users.Add(newUser);
                await _contextApp.SaveChangesAsync();
                return Created($"v1/user/{newUser.Id}", newUser);
            }
            catch (Exception)
            {
                return StatusCode(500, "Códido de erro: 02X03 - Erro ao salvar nivél de acesso!");
            }
        }
    }
}