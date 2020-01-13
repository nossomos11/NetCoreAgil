using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public ProAgilEventoRepository Repository { get; }

        public EventoController(ProAgilEventoRepository repository)
        {
            Repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> Get()
        {
            try{
                var eventos = await this.Repository.GetAllAsync(true);

                return Ok(eventos);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a busca da lista de eventos.");
            }
        }        

        // GET api/values
        [HttpGet("{eventoID}")]
        public async Task<ActionResult<Evento>> Get(int eventoID)
        {
            try{
                var evento = await this.Repository.Get(true, eventoID);

                return Ok(evento);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a busca do evento.");
            }
        } 

        // GET api/values
        [HttpGet("getByTema/{tema}")]
        public async Task<ActionResult<IEnumerable<Evento>>> Get(string tema)
        {
            try{
                var eventos = await this.Repository.GetAllAsync(true,tema);
                return Ok(eventos);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a busca do evento.");
            }
        }

        // GET api/values
        [HttpPost]
        public async Task<ActionResult> Post(Evento evento)
        {
            try{
                this.Repository.Add(evento);

                if(await this.Repository.SaveChangesAsync()){
                    return Created($"/api/evento/{evento.ID}", evento);
                }

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a criação do evento.");
            }

            return BadRequest();
        }         


        // GET api/values
        [HttpPut("{eventoID}")]
        public async Task<ActionResult> Put(int eventoID, Evento evento)
        {
            try{

                var oldEvento = this.Repository.Get(false, eventoID);
                if(oldEvento.Result == null){ return NotFound(); }


                oldEvento.Result.DataEvento = evento.DataEvento;
                oldEvento.Result.Email = evento.Email;
                oldEvento.Result.ImagemURL = evento.ImagemURL;
                oldEvento.Result.Local = evento.Local;
                oldEvento.Result.Lotes = evento.Lotes;
                oldEvento.Result.PalestrantesEventos = evento.PalestrantesEventos;
                oldEvento.Result.QtdPessoas = evento.QtdPessoas;
                oldEvento.Result.RedeSociais = evento.RedeSociais;
                oldEvento.Result.Telefone = evento.Telefone;
                oldEvento.Result.Tema = evento.Tema;

                this.Repository.Update(oldEvento.Result);

                if(await this.Repository.SaveChangesAsync()){
                    return Created($"/api/evento/{oldEvento.Result.ID}", oldEvento.Result);
                }

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a atualização do evento.");
            }

            return BadRequest();
        } 


        // GET api/values
        [HttpDelete("{eventoID}")]
        public async Task<ActionResult> Delete(int eventoID)
        {
            try{

                var oldEvento = this.Repository.Get(false, eventoID);
                if(oldEvento.Result == null){ return NotFound(); }

                this.Repository.Delete(oldEvento.Result);

                if(await this.Repository.SaveChangesAsync()){
                    return Ok();
                }

            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a exclusão do evento.");
            }

            return BadRequest();
        }                     
    }                            
 
}