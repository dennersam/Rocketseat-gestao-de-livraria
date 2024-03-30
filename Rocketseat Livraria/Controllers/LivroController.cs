using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Rocketseat_Livraria.Data;
using Rocketseat_Livraria.Models;
using System.Diagnostics.Eventing.Reader;

namespace Rocketseat_Livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LivroController(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }

        [HttpGet("Livros")]
        public ActionResult<List<Livro>> Get()
        {
            try
            {
                List<Livro> livros = _db.Livros.ToList();
                return Ok(livros);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public ActionResult<Livro> Get(int id)
        {
            Livro livro = _db.Livros.First(x => x.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }


        [HttpPost]
        public ActionResult<Livro> Post([FromBody] Livro livro)
        {
            if(livro == null)
            {
                return BadRequest();
            }

            try
            {
                _db.Livros.Add(new Livro() { 
                    Id = livro.Id, 
                    Autor = livro.Autor, 
                    Genero = livro.Genero, 
                    Preco = livro.Preco, 
                    Quantidade = livro.Quantidade,
                    Titulo = livro.Titulo
                });

                _db.SaveChanges();
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Livro livro)
        {
            if (livro == null) BadRequest();

            try
            {
                _db.Livros.Entry(livro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
            
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var livro = _db.Livros.Find(id);
            if (livro == null) return NoContent();
            
            _db.Livros.Remove(livro);
            _db.SaveChanges();

            return NoContent();
            
        }
    }
}
