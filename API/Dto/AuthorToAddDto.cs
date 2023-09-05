using System;
namespace API.Dto
{
	public class AuthorToAddDto
	{
		public string Name { get; set; }
        public string SurName { get; set; }

		public AuthorToAddDto()
		{

		}

        public AuthorToAddDto(string firstName, string lastName)
        {
			Name = firstName;
			SurName = lastName;
        }
    }
}

