using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using Api.Models.DTOs;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksControllers : ControllerBase
    {
        private readonly ApiDbContext context;

        public BooksControllers(ApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var books = await context.Books.ToListAsync();
                if (books != null)
                {
                    return Ok(books);
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
                var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    return Ok(book);
                }
                return NotFound($"No existe el registro con id: {id}");
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpGet("search/{title}")]
        public async Task<ActionResult> GetByName(string title)
        {
            try
            {
                title = title.Trim().ToLower();
                var books = await context.Books
                    .Where(x => x.Title.ToLower().Contains(title))
                    .ToListAsync();

                if (books != null)
                {
                    return Ok(books);
                }
                return NotFound("No se encontraron registros con ese titulo");
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(BookDto bookDto)
        {
            try
            {
                var book = Book.ConvertsDtoToDao(bookDto);

                context.Books.Add(book);
                await context.SaveChangesAsync();

                return Ok(book);
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, BookDto bookDto)
        {
            try
            {
                var exists = await context.Books.AnyAsync(x => x.Id == id);
                if (!exists)
                {
                    return NotFound();
                }

                var book = Book.ConvertsDtoToDao(bookDto, id);

                context.Books.Update(book);
                await context.SaveChangesAsync();

                return Ok(book);
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
                var bookToDelete = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (bookToDelete == null)
                {
                    return NotFound();
                }

                context.Books.Remove(bookToDelete);
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

