using System;
using System.Reflection.Emit;
using Core.Entities;

namespace Core.Specification
{
	public class PostSpecification : BaseSpecification<Post>
    {
        public PostSpecification(int id) : base(x =>
                id > 0 && x.Id == id)
        {
            AddInclude(x => x.Author);
        }

        public PostSpecification(string title, string desciption, int authorId) : base(x =>
                (string.IsNullOrEmpty(title) || x.Title == title) &&
                (string.IsNullOrEmpty(desciption) || x.Title == desciption) &&
                (authorId > 0 && x.AuthorId == authorId))
        {
            AddInclude(x => x.Author);
        }

        public PostSpecification(): base()
		{
			AddInclude(x => x.Author);
		}
	}
}

