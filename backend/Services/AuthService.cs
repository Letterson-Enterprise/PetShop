using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using backend_petshop.DTOs;
using backend_petshop.Interfaces;

namespace backend_petshop.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUsuarioRepository usuarioRepository, IOptions<JwtSettings> jwtSettings)
        {
            _usuarioRepository = usuarioRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var usuario = await _usuarioRepository.GetByLogin(loginDto.Login);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.SenhaHash))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Login),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                DataExpiracao = tokenDescriptor.Expires.Value
            };
        }
    }

    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public int ExpiracaoHoras { get; set; } = 8;
        public string Emissor { get; set; } = string.Empty;
        public string Audiencia { get; set; } = string.Empty;
    }
}
