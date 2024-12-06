namespace ApInsta.Domain.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Autentica um usuário com base nas credenciais fornecidas.
        /// </summary>
        /// <param name="login">Login do usuário (e-mail ou username).</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>Um token de autenticação JWT, se bem-sucedido; caso contrário, null.</returns>
        Task<string?> LoginAsync(string login, string password);

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="email">E-mail do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>O identificador do novo usuário, se bem-sucedido.</returns>
        Task<Guid> RegisterAsync(string name, string email, string password);
    }

}
