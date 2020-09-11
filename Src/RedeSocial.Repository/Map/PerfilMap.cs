using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Post;
using RedeSocial.Domain.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Repository.Map
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("Perfis");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.FotoPerfilUrl);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.SobreNome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.DataNascimento).IsRequired();

            builder.HasMany<Perfil>(x => x.IDs_Seguidores);
            builder.HasMany<Perfil>(x => x.IDs_Seguindo);

            builder.HasMany<Postagem>(x => x.IDs_Postagens);
        }
    }
}
