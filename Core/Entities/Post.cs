using System;
namespace Core.Entities
{
	public class Post: BaseEntity
	{
		public int? AuthorId { get; set; }
		public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public virtual Author Author { get; set; }
	}
}

