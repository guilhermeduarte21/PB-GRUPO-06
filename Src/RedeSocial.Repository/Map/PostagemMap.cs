using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Post;
using RedeSocial.Domain.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Repository.Map
{
    public class PostagemMap : IEntityTypeConfiguration<Postagem>
    {
        public void Configure(EntityTypeBuilder<Postagem> builder)
        {
            builder.ToTable("Portagens");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.FotoPostUrl);
            builder.Property(x => x.Descricao).IsRequired();

            builder.HasOne<Perfil>(x => x.ID_Perfil);
            builder.HasMany<Comentario>(x => x.IDs_Comentarios);
        }
    }
}
