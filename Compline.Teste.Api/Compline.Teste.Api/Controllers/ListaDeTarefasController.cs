using Compline.Teste.Api.Data_Access_Object;
using Compline.Teste.Commons.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace Compline.Teste.Api.Controllers
{
    public class ListaDeTarefasController : ApiController
    {
        private DAO<ListaDeTarefas> Dao;

        public ListaDeTarefasController()
        {
            this.Dao = new DAO<ListaDeTarefas>();
        }
        // GET: api/ListaDeTarefas
        [HttpGet]
        public IEnumerable<ListaDeTarefas> Get()
        {
            return Dao.GetAll();
        }

        // GET: api/ListaDeTarefas/5
        [HttpGet]
        [ResponseType(typeof(ListaDeTarefas))]
        public IHttpActionResult Get(int id)
        {
            var ListaDeTarefas = Dao.FindById(id);

            if (ListaDeTarefas == null)
                return NotFound();

            return Ok(ListaDeTarefas);
        }

        // POST: api/ListaDeTarefas
        [HttpPost]
        public IHttpActionResult Post(ListaDeTarefas ListaDeTarefas)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ListaDeTarefas.CreatedAt = DateTime.Now;
            ListaDeTarefas.UpdatedAt = DateTime.Now;

            var added = Dao.Add(ListaDeTarefas);

            return Ok(added);

        }

        // PUT: api/ListaDeTarefas/5
        [HttpPut]
        public IHttpActionResult Put(ListaDeTarefas ListaDeTarefas)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ListaDeTarefas.UpdatedAt = DateTime.Now;

            Dao.Update(ListaDeTarefas);

            return Get(ListaDeTarefas.Id);

        }

        // DELETE: api/ListaDeTarefas/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Dao.Remove(id);

            return Ok();
        }
    }
}
