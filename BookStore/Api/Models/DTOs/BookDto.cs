using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs
{
    public class BookDto
    {
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}

