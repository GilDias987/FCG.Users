using FCG.Users.Domain.Common.Exception;
using FCG.Users.Domain.Common.Validations;
using System.Drawing;
using System.Text.RegularExpressions;


namespace FCG.Users.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string nome, string email, string passoword, int userGroupId)
        {
            Inicializar(nome, email, passoword, userGroupId);
        }

        #region Propriedades Base
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Passoword { get; private set; }
        #endregion

        #region Propriedades de Navegação
        public int UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }
        #endregion

        public void Inicializar(string name, string email, string passoword, int userGroupId)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(name), "O nome do usuário não pode ser vazio.");
            Guard.AgainstEmptyId(userGroupId, "Grupo Usuario Id");

            Name = name.Trim();
            UserGroupId = userGroupId;
            Email = email;

            // Valid Passoword
            ValidPassoword(passoword.Trim());
            Passoword = BCrypt.Net.BCrypt.HashPassword(passoword);

            // Valid Email
            ValidEmail(email);
            ValidEmailDomain(email);
            Email = email;

        }

        public bool VerifyPassword(string passoword)
        {
            return BCrypt.Net.BCrypt.Verify(passoword, Passoword);
        }

        private static void ValidPassoword(string passoword)
        {
            Guard.Against<DomainException>(passoword.Length < 8, "Senha deve ter pelo menos 8 caracteres.");
            Guard.Against<DomainException>(!Regex.IsMatch(passoword, "[a-zA-Z]"), "Senha deve conter pelo menos uma letra.");
            Guard.Against<DomainException>(!Regex.IsMatch(passoword, "[0-9]"), "Senha deve conter pelo menos um número.");
            Guard.Against<DomainException>(!Regex.IsMatch(passoword, "[0-9]"), "Senha deve conter pelo menos um caractere especial.");
        }

        private static void ValidEmailDomain(string value)
        {
            Guard.Against<DomainException>(!Regex.IsMatch(value, @"@(fiap\.com\.br|alura\.com\.br|pm3\.com\.br)$"),
                                                                    "E-mail deve pertencer aos domínios @fiap.com.br, @alura.com.br ou @pm3.com.br.");
        }
        private static void ValidEmail(string value)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(value), "E-mail não pode ser vazio.");
        }
    }
}
