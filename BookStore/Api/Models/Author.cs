using System.ComponentModel.DataAnnotations;

using Api.Models.DTOs;

namespace Api.Models
{
    public class Author
    {
        public int? Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public static Author ConvertsDtoToDao(AuthorDto authorDto, int? id = null)
        {
            return new Author
            {
                Id = id,
                Firstname = authorDto.Firstname,
                Lastname = authorDto.Lastname,
            };
        }
    }
}

