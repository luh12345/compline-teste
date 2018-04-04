using Compline.Teste.Database.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compline.Teste.Database.Entity_Context
{
    public class EntityContext: DbContext
    {
        public DbSet<ListaDeTarefas> ListaDeTarefas { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>()
                .HasRequired<ListaDeTarefas>(t => t.ListaDeTarefas)
                .WithMany(lista => lista.Tarefas)
                .HasForeignKey<int>(t => t.Id);

            modelBuilder.Entity<Tarefa>().HasRequired(t => t.Prioridade);
        }
    }
}
