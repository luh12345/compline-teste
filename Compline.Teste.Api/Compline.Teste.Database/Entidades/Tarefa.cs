using System.ComponentModel.DataAnnotations;

namespace Compline.Teste.Database.Entidades
{
    public class Tarefa : BaseObject
    {
        [Required]
        public string Descricao { get; set; }

        public int PrioridadeId { get; set; }

        public Prioridade Prioridade { get; set; }

        public int ListaDeTarefasId { get; set; }

        public ListaDeTarefas ListaDeTarefas { get; set; }
    }
}