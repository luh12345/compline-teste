using Compline.Teste.Api.Data_Access_Object;
using Compline.Teste.Commons.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace Compline.Teste.Api.Controllers
{
    public class PrioridadesController : ApiController
    {
        private DAO<Prioridade> Dao;

        public PrioridadesController()
        {
            this.Dao = new DAO<Prioridade>();
        }
        // GET: api/Prioridades
        [HttpGet]
        public IEnumerable<Prioridade> Get()
        {
            return Dao.GetAll();
        }

        // GET: api/Prioridades/5
        [HttpGet]
        [ResponseType(typeof(Prioridade))]
        public IHttpActionResult Get(int id)
        {
            var prioridade = Dao.FindById(id);

            if (prioridade == null)
                return NotFound();

            return Ok(prioridade);
        }

        // POST: api/Prioridades
        [HttpPost]
        public IHttpActionResult Post(Prioridade prioridade)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            prioridade.CreatedAt = DateTime.Now;
            prioridade.UpdatedAt = DateTime.Now;

            var added = Dao.Add(prioridade);

            return Ok(added);

        }

        // PUT: api/Prioridades/5
        [HttpPut]
        public IHttpActionResult Put(Prioridade prioridade)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            prioridade.UpdatedAt = DateTime.Now;

            Dao.Update(prioridade);

            return Get(prioridade.Id);

        }

        // DELETE: api/Prioridades/5
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
