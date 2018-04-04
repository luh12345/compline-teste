using Compline.Teste.Commons.Entidades;
using Compline.Teste.Commons.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Compline.Teste.MVC.Services
{
    public class PrioridadesService : IService<Prioridade>
    {
        public async Task Add(Prioridade t)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiPrioridades"];

                using (HttpResponseMessage response = await client.PostAsJsonAsync<Prioridade>(endpoint, t))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception("Falha em adicionar uma nova lista de tarefas");
                }
            }
        }

        public async Task<Prioridade> FindById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = string.Format("{0}/{1}", ConfigurationManager.AppSettings["apiPrioridades"], id);

                using (HttpResponseMessage response = await client.GetAsync(endpoint))
                {
                    var jsonPrioridade = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<Prioridade>(jsonPrioridade);
                }
            }
        }

        public async Task<IEnumerable<Prioridade>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiPrioridades"];

                using (HttpResponseMessage response = await client.GetAsync(endpoint))
                {
                    var jsonPrioridades = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<Prioridade>>(jsonPrioridades);
                }
            }
        }

        public async Task Remove(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = string.Format("{0}/{1}", ConfigurationManager.AppSettings["apiPrioridades"], id);

                using (HttpResponseMessage reponse = await client.DeleteAsync(endpoint))
                {
                    if (!reponse.IsSuccessStatusCode)
                        throw new Exception("Falha ao remover");
                }
            }
        }

        public async Task Update(Prioridade t)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiPrioridades"];

                using (HttpResponseMessage response = await client.PutAsJsonAsync<Prioridade>(endpoint, t))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception("Falha ao atualizar os dados");
                }
            }
        }
    }
}