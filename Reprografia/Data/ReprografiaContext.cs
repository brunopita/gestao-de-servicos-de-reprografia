using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Reprografia.Models.Account;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Reprografia.Models;

namespace Reprografia.Data
{
    public class ReprografiaContext : DbContext
    {
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<ItemAvaliacao> ItemsAvaliacao { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Codificacao> Codificacoes { get; set; }

        public DbSet<EmailCobrancaAvaliacao> Emails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Solicitacao>()
                .HasOptional(s => s.Avaliacao)
                .WithRequired(a => a.Solicitacao);
            modelBuilder.Entity<Solicitacao>()
                .HasMany(s => s.Itens)
                .WithRequired(i => i.Solicitacao)
                .HasForeignKey(i => i.SolicitacaoId);
            modelBuilder.Entity<Avaliacao>()
                .HasMany(a => a.ItensAvaliacao)
                .WithRequired(i => i.Avaliacao)
                .HasForeignKey(i => i.AvaliacaoId);
            modelBuilder.Entity<Item>()
                .HasOptional(i => i.ItemAvaliacao)
                .WithRequired(i => i.Item)
                .WillCascadeOnDelete(false);

                

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
