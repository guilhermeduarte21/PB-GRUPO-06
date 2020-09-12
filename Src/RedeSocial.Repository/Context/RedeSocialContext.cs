using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using RedeSocial.Repository.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Repository.Context
{
    //Utiliza para buscar os dados no banco de dados
    public class RedeSocialContext : DbContext
    {
        //Busca os dados no banco de dados
        //Db set é uma proprieda do Entity Framework que controla toda a Interface do banco de dados, faz a interface das Query para o Banco de Dados
        public DbSet<Domain.Account.Account> Accounts { get; set; }
        public DbSet<Domain.Account.Role> Roles { get; set; }
        public DbSet<Domain.Profile.Perfil> Perfis { get; set; }
        public DbSet<Domain.Post.Postagem> Postagens { get; set; }
        public DbSet<Domain.Post.Comentario> Comentarios { get; set; }

        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public RedeSocialContext(DbContextOptions<RedeSocialContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new PostagemMap());
            modelBuilder.ApplyConfiguration(new ComentarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }

    /*
    public class BloggingContextFactory : IDesignTimeDbContextFactory<RedeSocialContext>
    {
        public RedeSocialContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RedeSocialContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RedeSocial;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new RedeSocialContext(optionsBuilder.Options);
        }
    }
    */
}
