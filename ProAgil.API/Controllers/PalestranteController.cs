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
    public class PalestranteController : ControllerBase
    {
        public ProAgilPalestranteRepository Repository { get; }

        public PalestranteController(ProAgilPalestranteRepository repository)
        {
            Repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Palestrante>>> Get()
        {
            try{
                var palestrantes = await this.Repository.GetAllAsync(true);

                return Ok(palestrantes);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a busca da lista de palestrantes.");
            }
        }        

        // GET api/values
        [HttpGet("{palestranteID}")]
        public async Task<ActionResult<Palestrante>> Get(int palestranteID)
        {
            try{
                var palestrante = await this.Repository.Get(true, palestranteID);

                return Ok(palestrante);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a busca do palestrante.");
            }
        } 

        // GET api/values
        [HttpGet("getByNome/{nome}")]
        public async Task<ActionResult<IEnumerable<Palestrante>>> Get(string nome)
        {
            try{
                var palestrantes = await this.Repository.GetAllAsync(true, nome);
                return Ok(palestrantes);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a busca da lista de palestrante.");
            }
        }

        // GET api/values
        [HttpPost]
        public async Task<ActionResult> Post(Palestrante palestrante)
        {
            try{
                this.Repository.Add(palestrante);

                if(await this.Repository.SaveChangesAsync()){
                    return Created($"/api/palestrante/{palestrante.ID}", palestrante);
                }

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a criação do palestrante.");
            }

            return BadRequest();
        }         


        // GET api/values
        [HttpPut("{palestranteID}")]
        public async Task<ActionResult> Put(int palestranteID, Palestrante palestrante)
        {
            try{

                var oldPalestrante = this.Repository.Get(false, palestranteID);
                if(oldPalestrante.Result == null){ return NotFound(); }


                oldPalestrante.Result.Nome = palestrante.Nome;
                oldPalestrante.Result.Email = palestrante.Email;
                oldPalestrante.Result.ImagemURL = palestrante.ImagemURL;
                oldPalestrante.Result.Miniurriculo = palestrante.Miniurriculo;
                oldPalestrante.Result.PalestrantesEventos = palestrante.PalestrantesEventos;
                oldPalestrante.Result.RedeSociais = palestrante.RedeSociais;
                oldPalestrante.Result.Telefone = palestrante.Telefone;

                this.Repository.Update(oldPalestrante.Result);

                if(await this.Repository.SaveChangesAsync()){
                    return Created($"/api/evento/{oldPalestrante.Result.ID}", oldPalestrante.Result);
                }

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a atualização do palestrante.");
            }

            return BadRequest();
        } 


        // GET api/values
        [HttpDelete("{palestranteID}")]
        public async Task<ActionResult> Delete(int palestranteID)
        {
            try{

                var oldPalestrante = this.Repository.Get(false, palestranteID);
                if(oldPalestrante.Result == null){ return NotFound(); }

                this.Repository.Delete(oldPalestrante.Result);

                if(await this.Repository.SaveChangesAsync()){
                    return Ok();
                }

            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro durante a exclusão do palestrante.");
            }

            return BadRequest();
        }                     
    }                            
 
}