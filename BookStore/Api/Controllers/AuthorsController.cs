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
            try
            {
                var authors = await context.Authors.ToListAsync();
                if (authors != null)
                {
                    return Ok(authors);
                }
                return NoContent();
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == id);
                if (author != null)
                {
                    return Ok(author);
                }
                return NotFound($"No existe el registro con id: {id}");
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            try
            {
                name = name.Trim().ToLower();
                var authors = await context.Authors
                    .Where(x => x.Firstname.ToLower().Contains(name) || x.Lastname.ToLower().Contains(name))
                    .ToListAsync();

                if (authors != null)
                {
                    return Ok(authors);
                }
                return NotFound("No se encontraron registros con ese nombre");
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(AuthorDto authorDto)
        {
            try
            {
                var author = Author.ConvertsDtoToDao(authorDto);

                context.Authors.Add(author);
                await context.SaveChangesAsync();

                return Ok(author);
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AuthorDto authorDto)
        {
            try
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
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var authorToDelete = await context.Authors.FirstOrDefaultAsync(x => x.Id == id);
                if (authorToDelete == null)
                {
                    return NotFound();
                }

                context.Authors.Remove(authorToDelete);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }
    }
}

