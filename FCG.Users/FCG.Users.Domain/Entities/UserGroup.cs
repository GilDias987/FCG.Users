using FCG.Users.Domain.Common.Exception;
using FCG.Users.Domain.Common.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Domain.Entities
{
    public class UserGroup : BaseEntity
    {
        public UserGroup(string name)
        {
            Inicializar(name);
        }

        #region Propriedades Base
        public string Name { get; private set; }
        #endregion

        #region Propriedades Navegacao
        public ICollection<User>? Users { get; set; }
        #endregion

        public void Inicializar(string name)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(name), "O nome do grupo não pode ser vazio.");
            Name = name.Trim();
        }
    }
}
