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
            var books = await context.Books.ToListAsync();
            if (books != null)
            {
                return Ok(books);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book != null)
            {
                return Ok(book);
            }
            return NotFound($"No existe el registro con id: {id}");
        }

        [HttpGet("search/{title}")]
        public async Task<ActionResult> GetByName(string title)
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

        [HttpPost]
        public async Task<ActionResult> Post(BookDto bookDto)
        {
            var book = Book.ConvertsDtoToDao(bookDto);

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, BookDto bookDto)
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Books.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Books.Remove(new Book(null, null, id));
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
