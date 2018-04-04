using Compline.Teste.Api.Entity_Context;
using Compline.Teste.Commons.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;

namespace Compline.Teste.Api.Data_Access_Object
{
    public class DAO<T> where T : BaseObject
    {
        private EntityContext Context;

        public DAO()
        {
            this.Context = new EntityContext();
        }

        public List<T> GetAll()
        {
            return this.Context.Set<T>().ToList();
        }

        public T FindById(int id)
        {
            var entity = this.Context.Set<T>().Find(id);

            return entity;
        }

        public T Add(T t)
        {
            var added =  this.Context.Set<T>().Add(t);
            Context.SaveChanges();

            return added;
        }

        public void Update(T t)
        {
            try
            {
                var toUpdate = FindById(t.Id);

                if (toUpdate == null)
                    throw new ArgumentNullException("Objeto que tentou atualizar nao existe");

                this.Context.Entry(toUpdate).State = EntityState.Modified;
                this.Context.Entry(toUpdate).CurrentValues.SetValues(t);
                this.Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new UpdateException("Falha ao atualizar", e);
            }             
        }

        public void Remove(int id)
        {
            try
            {
                var toRemove = FindById(id);

                if (toRemove == null)
                    throw new ArgumentNullException("Objeto que tentou remover não existe");

                this.Context.Set<T>().Remove(toRemove);
                this.Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new UpdateException("Falha ao remover", e);
            }
        }
    }
}