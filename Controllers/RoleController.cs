using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relação1N.Data;
using Relação1N.Models;

namespace Relação1N.Controllers
{
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ContextApp _contextApp;

        public RoleController(ContextApp contextApp)
        {
            _contextApp = contextApp;
        }

        [HttpGet("/v1/roles")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var roles = await _contextApp.Roles
                    .Include(x => x.Users)
                    .AsNoTracking()
                    .Select(role => new
                    {
                        role.Id,
                        role.Name,
                        role.Description,
                        Users = role.Users.Select(user => new
                        {
                            user.Id,
                            user.Name
                        }).ToList() // Apenas ID e Nome dos usuários
                    })
                    .ToListAsync();

                return Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode(500, "Código de erro: 02X01 - Erro ao buscar tipos!");
            }
        }

        [HttpPost("/v1/roles")]
        public async Task<IActionResult> PostAsync([FromBody] Role model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _contextApp.Roles.AddAsync(model);
                await _contextApp.SaveChangesAsync();
                return Created($"v1/roles/{model.Id}", model);
            }
            catch (Exception)
            {
                return StatusCode(500, "Códido de erro: 02X03 - Erro ao salvar nivél de acesso!");
            }
        }
    }
}