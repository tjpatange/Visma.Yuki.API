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

        public PostSpecification(): base()
		{
			AddInclude(x => x.Author);
		}
	}
}

