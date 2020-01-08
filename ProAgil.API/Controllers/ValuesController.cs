using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Data;
using ProAgil.API.Model;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly DataContext DataContext;

        public ValuesController(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> Get()
        {
            try{
                var eventos = await DataContext.Eventos.ToAsyncEnumerable().ToList();

                return Ok(eventos);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a busca da lista de eventos.");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id)
        {
            try{
                var evento = await DataContext.Eventos.ToAsyncEnumerable().FirstOrDefault(x => x.EventoID == id);
                return Ok(evento);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a busca do evento informado.");
            }            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Evento value)
        {
            try{
                DataContext.Eventos.Add(value);
                DataContext.SaveChanges();
            }
            catch(Exception e)
            {
               throw new Exception("Ocorreu um erro durante a criação do evento!", e); 
            }            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Evento value)
        {
            try{
                var evento = DataContext.Eventos.FirstOrDefault(x => x.EventoID == id);
                if(evento != null){
                    evento.Tema = value.Tema;
                    evento.DataEvento = value.DataEvento;
                    evento.QtdPessoas = value.QtdPessoas;
                    evento.Local = value.Local;
                    evento.Lote = value.Lote;
                    DataContext.SaveChanges();
                }
            }
            catch(Exception e)
            {
               throw new Exception("Ocorreu um erro durante a atualização do evento!", e); 
            }            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try{
                var evento = DataContext.Eventos.FirstOrDefault(x => x.EventoID == id);
                if(evento != null){
                    DataContext.Eventos.Remove(evento);
                    DataContext.SaveChanges();
                }
            }
            catch(Exception)
            {

            }            
        }
    }
}
