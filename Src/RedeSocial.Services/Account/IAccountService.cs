using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Services.Account
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAsync(Domain.Account.Account user, CancellationToken cancellationToken);
    }
}
