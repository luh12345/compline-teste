using Compline.Teste.Commons.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compline.Teste.Commons.Interfaces
{
    public interface IService<T> where T: BaseObject
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> FindById(int id);
        Task Add(T t);
        Task Update(T t);
        Task Remove(int id);
    }
}
