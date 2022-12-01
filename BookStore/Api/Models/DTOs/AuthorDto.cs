namespace Api.Models.DTOs
{
    public class AuthorDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public AuthorDto(string firstname, string lastname )
        {
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}

