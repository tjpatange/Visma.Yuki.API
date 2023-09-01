using System;
using Core.Entities;

namespace Core.Specification
{
	public class AuthorSpecification: BaseSpecification<Author>
	{
        public AuthorSpecification(int id) : base(x => (id > 0 && x.Id == id))
        {
        }

        public AuthorSpecification(string name, string surname) : base(x =>
            (string.IsNullOrEmpty(name) || x.Name == name) &&
            (string.IsNullOrEmpty(surname) || x.SurName == surname))
        {
        }
        public AuthorSpecification(): base()
		{
		}
	}
}

