using System;
using Microsoft.Extensions.DependencyInjection;

namespace Visma.Yuki.XUnitTest.Fixtures
{
	public class ServiceCollectionFixture: IDisposable
	{
        public ServiceCollection collection { get; }
		public ServiceCollectionFixture()
		{
            collection = new ServiceCollection();
		}

        public void Dispose()
        {
            
        }
    }
}

