using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Post;
using RedeSocial.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Repository.Map
{
    public class PostagemMap : IEntityTypeConfiguration<Postagem>
    {
        public void Configure(EntityTypeBuilder<Postagem> builder)
        {
            builder.ToTable("Postagens");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.FotoPostUrl);
            builder.Property(x => x.Descricao).IsRequired();
            builder.Property(x => x.DataPostagem).IsRequired();

            builder.HasOne<Domain.Account.Account>(x => x.ID_Account);
            builder.HasMany<Comentario>(x => x.IDs_Comentarios);
        }
    }
}
