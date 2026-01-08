using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        public DateTime DateCreation { get; private set; }
        public DateTime DateUpdate { get; private set; }

        public void SetDateCreation(DateTime dateCreation, DateTime dateUpdate)
        {
            DateCreation = dateCreation;
            DateUpdate = dateUpdate;
        }
        public void SetDateUpdate(DateTime dataAtualizacao)
        {
            DateCreation = dataAtualizacao;
        }
    }
}
