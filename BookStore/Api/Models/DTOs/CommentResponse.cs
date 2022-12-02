using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs
{
    public class CommentResponse
    {
        public int? Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}

