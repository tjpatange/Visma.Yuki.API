using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Extensions;
using Core.Interfaces;
using Core.Entities;
using Visma.Yuki.XUnitTest.Fixtures;

namespace Visma.Yuki.XUnitTest;

public class ServiceRegistrationExtensions: IClassFixture<ServiceCollectionFixture>
{
    private readonly ServiceCollectionFixture _fixture;


    public ServiceRegistrationExtensions(ServiceCollectionFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void AddApplicationServices_Execute_CheckDbContextIsRegistered()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string> {
                        {"ConnectionStrings:DefaultConnection", "AnyValueWillDo"}})
            .Build();

        // Act
        _fixture.collection.AddApplicationServices(configuration);
        var serviceProvider = _fixture.collection.BuildServiceProvider();

        var dbContext = serviceProvider.GetService<StoreContext>();

        Assert.NotNull(dbContext);
        Assert.IsType<StoreContext>(dbContext);
    }

    [Fact]
    public void AddApplicationServices_Execute_CheckIUnitOfWorkServiceIsRegistered()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().Build();

        // Act
        _fixture.collection.AddApplicationServices(configuration);
        var serviceProvider = _fixture.collection.BuildServiceProvider();

        // Assert
        Assert.NotNull(
            serviceProvider.GetService<IUnitOfWork>());
        Assert.IsType<UnitOfWork>(
            serviceProvider.GetService<IUnitOfWork>());

    }
}
