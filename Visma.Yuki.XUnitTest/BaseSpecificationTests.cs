using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification;

namespace Visma.Yuki.XUnitTest
{
	public class BaseSpecificationTests 
    {
		[Fact]
		public void ConstructorWithCriteria_SetsPropertyCriteria()
		{
			// Arrange
			Expression<Func<Author, bool>> criteria = entity => entity.Id == 1;

            // Act
            var authorSpecification = new BaseSpecification<Author>(criteria);

			// Assert
			Assert.Equal(criteria, authorSpecification.Criteria);

        }

        [Fact]
        public void AddInclude_IncludeExpressions_ToInclude()
        {
            // Arrange
            var authorSpecification = new BaseSpecification<Author>();
            Expression<Func<Author, object>> includeExpression = entity => entity.Posts;

            // Act
            authorSpecification.Includes.Add(includeExpression);

            // Assert
            Assert.Contains(includeExpression, authorSpecification.Includes);

        }

        [Fact]
        public void AddInclude_IncludeStringList_ToInclude()
        {
            // Arrange
            var authorSpecification = new BaseSpecification<Author>();
            string includeString = "Post";

            // Act
            authorSpecification.IncludeStrings.Add(includeString);

            // Assert
            Assert.Contains(includeString, authorSpecification.IncludeStrings);

        }


    }
}

