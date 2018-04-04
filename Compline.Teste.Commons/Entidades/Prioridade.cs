using System.ComponentModel.DataAnnotations;


namespace Compline.Teste.Commons.Entidades
{
    public class Prioridade : BaseObject
    {
        [Required]
        public string Descricao { get; set; }
    }
}