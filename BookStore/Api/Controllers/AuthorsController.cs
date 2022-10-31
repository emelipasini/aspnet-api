using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<Author>>> Get()
        {
            return await context.Authors.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Author author)
        {
            context.Authors.Add(author);
            await context.SaveChangesAsync();

            return Ok(author);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Author author)
        {
            var exists = await context.Authors.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Update(author);
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

            context.Remove(new Author(id, null, null));
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}

