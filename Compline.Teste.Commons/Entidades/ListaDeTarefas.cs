using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Compline.Teste.Commons.Entidades
{
    public class ListaDeTarefas : BaseObject
    {
        [Required]
        public string NomeLista { get; set; }

        public List<Tarefa> Tarefas { get; set; }

    }
}