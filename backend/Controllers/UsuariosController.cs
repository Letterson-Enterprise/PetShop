using Microsoft.AspNetCore.Mvc;
using backend_petshop.DTOs;
using backend_petshop.Interfaces;
using backend_petshop.Entities;

namespace backend_petshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUsuarioDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Login))
                return BadRequest(new ErrorResponse { Mensagem = "Login é obrigatório." });

            if (string.IsNullOrWhiteSpace(dto.Senha) || dto.Senha.Length < 5)
                return BadRequest(new ErrorResponse { Mensagem = "Senha deve ter no mínimo 5 caracteres." });

            if (await _usuarioRepository.LoginExists(dto.Login))
                return Conflict(new ErrorResponse { Mensagem = "Login já existe." });

            var usuario = new Usuario
            {
                Login = dto.Login,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            await _usuarioRepository.Create(usuario);
            return Created();
        }
    }
}
