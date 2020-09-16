using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Domain.Account
{
    public class Role
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public virtual IList<Account> IDs_Accounts { get; set; } = new List<Account>(); //UMA ROLE TEM MUITAS CONTAS | UMA CONTA SO PODE TER UMA ROLE (1 : M)
    }
}
