using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compline.Teste.Database.Entidades
{
    public class ListaDeTarefas : BaseObject
    {
        [Required]
        public string NomeLista { get; set; }

        public List<Tarefa> Tarefas { get; set; }
        
    }
}
