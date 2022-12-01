using System.ComponentModel.DataAnnotations;

using Api.Models.DTOs;

namespace Api.Models
{
    public class Book
    {
        public int? Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public Book(string title, string description, int? id)
        {
            if(id != null)
            {
                Id = id;
            }

            Title = title;
            Description = description;
        } 

        public static Book ConvertsDtoToDao(BookDto bookDto, int? id = null)
        {
            return new Book(bookDto.Title, bookDto.Description, id);
        }
    }
}

