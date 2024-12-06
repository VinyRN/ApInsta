using ApInsta.Domain.Entities;
using ApInsta.Domain.Interfaces;
using ApInsta.Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApInsta.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(string login, string password)
        {
            var user = await _userRepository.GetByLoginAsync(login);

            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return GenerateJwtToken(user);
        }

        public async Task<Guid> RegisterAsync(string name, string email, string password)
        {
            // Verificar se já existe um usuário com o mesmo e-mail
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser is not null)
                throw new InvalidOperationException("Email já está em uso.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(Guid.NewGuid(), name, email, passwordHash);

            await _userRepository.AddAsync(user);

            return user.Id;
        }

        private string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Token ID único
                new Claim("name", user.Name), // Adicionar informações personalizadas
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2), // Token expira em 2 horas
                SigningCredentials = credentials
            };

            // Gerar o Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
