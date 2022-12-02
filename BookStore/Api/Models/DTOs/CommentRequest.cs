using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs
{
    public class CommentDto
    {
        [Required]
        public string Content { get; set; }
    }
}

