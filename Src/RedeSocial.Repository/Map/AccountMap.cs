using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Post;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RedeSocial.Repository.Map
{
    public class AccountMap : IEntityTypeConfiguration<Domain.Account.Account>
    {
        public void Configure(EntityTypeBuilder<Domain.Account.Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(150);
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.FotoPerfilUrl);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.SobreNome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.DataNascimento).IsRequired();


            builder.HasOne<Role>(x => x.ID_Role);

            builder.HasMany<Domain.Account.Account>(x => x.IDs_Seguidores);
            builder.HasMany<Domain.Account.Account>(x => x.IDs_Seguindo);

            builder.HasMany<Postagem>(x => x.IDs_Postagens);
        }
    }
}
