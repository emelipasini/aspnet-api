using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using Api.Models.DTOs;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ApiDbContext context;

        public AuthorsController(ApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var authors = await context.Authors.ToListAsync();
            if(authors != null)
            {
                return Ok(authors);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if(author != null)
            {
                return Ok(author);
            }
            return NotFound($"No existe el registro con id: {id}");
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            name = name.Trim().ToLower();
            var authors = await context.Authors
                .Where(x => x.Firstname.ToLower().Contains(name) || x.Lastname.ToLower().Contains(name))
                .ToListAsync();

            if(authors != null)
            {
                return Ok(authors);
            }
            return NotFound("No se encontraron registros con ese nombre");
        }

        [HttpPost]
        public async Task<ActionResult> Post(AuthorDto authorDto)
        {
            var author = Author.ConvertsDtoToDao(authorDto);

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            return Ok(author);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AuthorDto authorDto)
        {
            var exists = await context.Authors.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            var author = Author.ConvertsDtoToDao(authorDto, id);

            context.Authors.Update(author);
            await context.SaveChangesAsync();

            return Ok(author);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Authors.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Authors.Remove(new Author(null, null, id));
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}

