using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase {
        public ProAgilEventoRepository Repository { get; }
        public IMapper Mapper { get; }

        public EventoController (ProAgilEventoRepository repository, IMapper mapper) {
            this.Mapper = mapper;
            this.Repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> Get () {
            try {
                var eventos = await this.Repository.GetAllAsync (true);
                var results = this.Mapper.Map<IEnumerable<EventoDTO>>(eventos);

                return Ok (results);
            } catch (Exception ex) {
                return StatusCode (StatusCodes.Status500InternalServerError, $"Ocorreu um erro durante a busca da lista de eventos. {ex.Message}");
            }
        }

        // GET api/values
        [HttpGet ("{eventoID}")]
        public async Task<ActionResult<Evento>> Get (int eventoID) {
            try {
                var evento = await this.Repository.Get (true, eventoID);
                var result = this.Mapper.Map<EventoDTO>(evento);

                return Ok (result);
            } catch (Exception ex) {
                return StatusCode (StatusCodes.Status500InternalServerError, $"Ocorreu um erro durante a busca do evento. {ex.Message}");
            }
        }

        // GET api/values
        [HttpGet ("getByTema/{tema}")]
        public async Task<ActionResult<IEnumerable<Evento>>> Get (string tema) {
            try {
                var eventos = await this.Repository.GetAllAsync (true, tema);
                var results = this.Mapper.Map<IEnumerable<EventoDTO>>(eventos);
                return Ok (results);
            } catch (Exception ex) {
                Console.Write(ex.ToString());
                return StatusCode (StatusCodes.Status500InternalServerError, $"Ocorreu um erro durante a busca do evento. {ex.Message}");
            }
        }

        // GET api/values
        [HttpPost]
        public async Task<ActionResult> Post (EventoDTO eventoDTO) {
            try {

                var evento = this.Mapper.Map<Evento>(eventoDTO);
                this.Repository.Add (evento);

                if (await this.Repository.SaveChangesAsync ()) {
                    return Created ($"/api/evento/{evento.ID}", this.Mapper.Map<EventoDTO>(evento));
                }

            } catch (Exception ex) {
                Console.Write(ex.ToString());
                return StatusCode (StatusCodes.Status500InternalServerError, $"Ocorreu um erro durante a criação do evento. {ex.Message}");
            }

            return BadRequest ();
        }

        // GET api/values
        [HttpPut ("{eventoID}")]
        public async Task<ActionResult> Put (int eventoID, EventoDTO eventoDTO) {
            try {

                var evento = this.Repository.Get (false, eventoID).Result;
                if (evento == null) { return NotFound (); }

                this.Mapper.Map(eventoDTO, evento);

                this.Repository.Update (evento);

                if (await this.Repository.SaveChangesAsync ()) {
                    return Created ($"/api/evento/{evento.ID}", this.Mapper.Map<EventoDTO>(evento));
                }

            } catch (Exception ex) {
                Console.Write(ex.ToString());
                return StatusCode (StatusCodes.Status500InternalServerError, $"Ocorreu um erro durante a atualização do evento. {ex.Message}");
            }

            return BadRequest ();
        }

        // GET api/values
        [HttpDelete ("{eventoID}")]
        public async Task<ActionResult> Delete (int eventoID) {
            try {

                var oldEvento = this.Repository.Get (false, eventoID);
                if (oldEvento.Result == null) { return NotFound (); }

                this.Repository.Delete (oldEvento.Result);

                if (await this.Repository.SaveChangesAsync ()) {
                    return Ok ();
                }

            } catch (Exception ex) {
                Console.Write (ex.ToString ());
                return StatusCode (StatusCodes.Status500InternalServerError, $"Ocorreu um erro durante a exclusão do evento. {ex.Message}");
            }

            return BadRequest ();
        }
       
    }

}