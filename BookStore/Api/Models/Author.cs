using Api.Models.DTOs;

namespace Api.Models
{
    public class Author
    {
        public int? Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public Author(string firstname, string lastname, int? id)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;

            if(id != null)
            {
                this.Id = id;
            }
        }

        public static Author ConvertsDtoToDao(AuthorDto authorDto, int? id = null)
        {
            return new Author(authorDto.Firstname, authorDto.Lastname, id);
        }
    }
}

