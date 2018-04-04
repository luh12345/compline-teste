using Compline.Teste.Api.Data_Access_Object;
using Compline.Teste.Commons.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace Compline.Teste.Api.Controllers
{
    public class TarefasController : ApiController
    {
        private DAO<Tarefa> Dao;

        public TarefasController()
        {
            this.Dao = new DAO<Tarefa>();
        }
        // GET: api/Tarefas
        [HttpGet]
        public IEnumerable<Tarefa> Get()
        {
            return Dao.GetAll();
        }

        // GET: api/Tarefas/5
        [HttpGet]
        [ResponseType(typeof(Tarefa))]
        public IHttpActionResult Get(int id)
        {
            var Tarefa = Dao.FindById(id);

            if (Tarefa == null)
                return NotFound();

            return Ok(Tarefa);
        }

        // POST: api/Tarefas
        [HttpPost]
        public IHttpActionResult Post(Tarefa Tarefa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Tarefa.CreatedAt = DateTime.Now;
            Tarefa.UpdatedAt = DateTime.Now;

            var added = Dao.Add(Tarefa);

            return Ok(added);

        }

        // PUT: api/Tarefas/5
        [HttpPut]
        public IHttpActionResult Put(Tarefa Tarefa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Tarefa.UpdatedAt = DateTime.Now;

            Dao.Update(Tarefa);

            return Get(Tarefa.Id);

        }

        // DELETE: api/Tarefas/5
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
