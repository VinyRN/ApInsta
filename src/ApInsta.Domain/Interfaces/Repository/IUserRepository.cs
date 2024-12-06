using ApInsta.Domain.Entities;

namespace ApInsta.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Busca um usuário pelo login (e-mail ou nome de usuário).
        /// </summary>
        /// <param name="login">Login do usuário.</param>
        /// <returns>O usuário correspondente ou null, caso não exista.</returns>
        Task<User?> GetByLoginAsync(string login);

        /// <summary>
        /// Busca um usuário pelo e-mail.
        /// </summary>
        /// <param name="email">E-mail do usuário.</param>
        /// <returns>O usuário correspondente ou null, caso não exista.</returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Adiciona um novo usuário ao repositório.
        /// </summary>
        /// <param name="user">Entidade do usuário a ser adicionada.</param>
        Task AddAsync(User user);

        /// <summary>
        /// Atualiza as informações de um usuário existente.
        /// </summary>
        /// <param name="user">Usuário com as informações atualizadas.</param>
        Task UpdateAsync(User user);

        /// <summary>
        /// Remove um usuário do repositório.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido.</param>
        Task DeleteAsync(Guid id);
    }
}
