using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relação1N.Data;
using Relação1N.Models;

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
                var users = await _contextApp.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "Códido de erro: 02X01 - Erro ao buscar tipos!");
            }
        }

        [HttpPost("/v1/user")]
        public async Task<IActionResult> PostAsync([FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _contextApp.Users.AddAsync(model);
                await _contextApp.SaveChangesAsync();
                return Created($"v1/user/{model.Id}", model);
            }
            catch (Exception)
            {
                return StatusCode(500, "Códido de erro: 02X03 - Erro ao salvar tipos!");
            }
        }
    }
}