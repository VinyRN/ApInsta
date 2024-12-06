using Microsoft.Extensions.Configuration;
using FluentAssertions;
using Moq;
using ApInsta.Domain.Interfaces.Repository;
using ApInsta.Service;
using ApInsta.Domain.Entities;

namespace ApInsta.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration>  _configurationMock;
        private readonly AuthService _authService;

        private const string _name  = "fulano";
        private const string _email = $"{_name}@{_name}.com";
        private string password = "senha000123";
        private readonly string _passwordHash;


        //Login
        public AuthServiceTests() 
        { 
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock.Setup(config => config["Jwt:Key"]).Returns("9hK/dVqzL12OYFsF9r2+qT0WEkRaZjF6Cg==");

            _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);

            _passwordHash = BCrypt.Net.BCrypt.HashPassword("senha000123");
        }


        [Fact]
        public async Task LoginAsync_Valid()
        {
            

            User user = new User(Guid.NewGuid(),"Fulano","fulano@fulano.com",_passwordHash);

            _userRepositoryMock
                .Setup(repo => repo.GetByLoginAsync(_email))
                .ReturnsAsync(user);

            string? token = await _authService.LoginAsync(_email, password);

            token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task LoginAsync_Invalid()
        {
            _userRepositoryMock
                .Setup(repo => repo.GetByLoginAsync(_email))
                .ReturnsAsync((User?)null);

            string? token = await _authService.LoginAsync(_email, _passwordHash);
                
            token.Should().BeNull();
        }


        //Register
        [Fact]
        public async Task RegisterAsync_Valid()
        {
            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(_email))
                .ReturnsAsync((User?)null);

            Guid userId = await _authService.RegisterAsync(_name, _email,_passwordHash);

            userId.Should().NotBeEmpty();
        }

        [Fact]
        public async Task RegisterAsync_Exists()
        {
            var existingUser = new User(Guid.NewGuid(), _name, _email, _passwordHash);

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(_email))
                .ReturnsAsync(existingUser);


            var act = async () => await _authService.RegisterAsync(_name, _email, _passwordHash);

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Email já está em uso.");
        }
    }
}
