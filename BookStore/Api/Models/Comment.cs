using System.ComponentModel.DataAnnotations;

using Api.Models.DTOs;

namespace Api.Models
{
    public class Comment
    {
        public int? Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }

        public static Comment ConvertsDtoToDao(CommentDto commentDto, int bookId, int? id = null)
        {
            return new Comment
            {
                Id = id,
                Content = commentDto.Content,
                BookId = bookId
            };
        }

        public static CommentResponse ConvertsToResponse(Comment comment)
        {
            return new CommentResponse
            {
                Id = comment.Id,
                Content = comment.Content
            };
        }
    }
}

