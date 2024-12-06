namespace ApInsta.Domain.Entities
{
    public class User
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Nome completo do usuário.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Senha criptografada do usuário.
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Foto de perfil do usuário.
        /// </summary>
        public string? ProfilePictureUrl { get; private set; }

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Data da última modificação do registro.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        // Construtor público para criação inicial
        public User(Guid id, string name, string email, string passwordHash, string? profilePictureUrl = null)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            ProfilePictureUrl = profilePictureUrl;
            CreatedAt = DateTime.UtcNow;
        }

        // Método para atualizar a foto de perfil
        public void UpdateProfilePicture(string newProfilePictureUrl)
        {
            ProfilePictureUrl = newProfilePictureUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        // Método para alterar o nome
        public void UpdateName(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        // Método para alterar a senha (assumindo que a senha já está criptografada)
        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
