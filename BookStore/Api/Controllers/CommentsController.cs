using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Api.Models.DTOs;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/books/{bookId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ApiDbContext context;

        public CommentsController(ApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int bookId)
        {
            try
            {
                var bookExists = await context.Books.AnyAsync(x => x.Id == bookId);

                if (!bookExists)
                {
                    return NotFound($"No se encontraron registros con id: {bookId}");
                }

                var comments = await context.Comments.Where(comment => comment.BookId == bookId).ToListAsync();

                var response = new List<CommentResponse>();

                foreach (Comment comment in comments)
                {
                    response.Add(Comment.ConvertsToResponse(comment));
                }

                return Ok(response);
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(int bookId, CommentDto commentDto)
        {
            try
            {
                var bookExists = await context.Books.AnyAsync(x => x.Id == bookId);

                if (!bookExists)
                {
                    return NotFound($"No se encontraron registros con id: {bookId}");
                }

                var comment = Comment.ConvertsDtoToDao(commentDto, bookId);

                context.Add(comment);
                await context.SaveChangesAsync();

                var response = Comment.ConvertsToResponse(comment);

                return Ok(response);
            }
            catch (Exception err)
            {
                return Problem($"Ha ocurrido un error al realizar la transaccion. {err}");
            }
        }
    }
}

