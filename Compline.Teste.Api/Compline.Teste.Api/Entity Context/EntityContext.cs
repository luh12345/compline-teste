using Compline.Teste.Commons.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Compline.Teste.Api.Entity_Context
{
    public class EntityContext : DbContext
    {
        public DbSet<ListaDeTarefas> ListaDeTarefas { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListaDeTarefas>()
                .HasMany(lista => lista.Tarefas)
                .WithRequired(t => t.ListaDeTarefas)
                .HasForeignKey(t => t.ListaDeTarefasId);

            modelBuilder.Entity<Tarefa>().HasRequired(t => t.Prioridade);
        }
    }
}