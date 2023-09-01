using System;
namespace API.Dto
{
	public class PostToListDto
	{
		public int Id { get; set; }
		public AuthorToListDto Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

    }


}

