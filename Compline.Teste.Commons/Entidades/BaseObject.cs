using System;

namespace Compline.Teste.Commons.Entidades
{
    public abstract class BaseObject
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}