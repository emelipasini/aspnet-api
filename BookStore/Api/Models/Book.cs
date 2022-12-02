using System.ComponentModel.DataAnnotations;

using Api.Models.DTOs;

namespace Api.Models
{
    public class Book
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public List<Comment>? Comments { get; set; }

        public static Book ConvertsDtoToDao(BookDto bookDto, int? id = null)
        {
            return new Book
            {
                Id = id,
                Title = bookDto.Title,
                Description = bookDto.Description
            };
        }
    }
}

