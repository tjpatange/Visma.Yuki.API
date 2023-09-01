using System;
using System.Collections.Generic;

namespace Core.Entities
{
	public class Author:  BaseEntity
	{
		public string Name { get; set; }
        public string SurName { get; set; }
		public ICollection<Post> Posts { get; set; }
	}
}

