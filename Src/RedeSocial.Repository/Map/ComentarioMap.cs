using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Post;
using RedeSocial.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Repository.Map
{
    public class ComentarioMap : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Comentarios");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao);

            builder.HasOne<Domain.Account.Account>(x => x.ID_Account);
            builder.HasOne<Postagem>(x => x.ID_Postagem);
        }
    }
}
