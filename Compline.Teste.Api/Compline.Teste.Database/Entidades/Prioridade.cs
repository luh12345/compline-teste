using System.ComponentModel.DataAnnotations;

namespace Compline.Teste.Database.Entidades
{
    public class Prioridade : BaseObject
    {
        [Required]
        public string Descricao { get; set; }
    }
}