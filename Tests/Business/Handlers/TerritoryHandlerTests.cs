
using Business.Handlers.Territories.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Territories.Queries.GetTerritoryQuery;
using Entities.Concrete;
using static Business.Handlers.Territories.Queries.GetTerritoriesQuery;
using static Business.Handlers.Territories.Commands.CreateTerritoryCommand;
using Business.Handlers.Territories.Commands;
using Business.Constants;
using static Business.Handlers.Territories.Commands.UpdateTerritoryCommand;
using static Business.Handlers.Territories.Commands.DeleteTerritoryCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class TerritoryHandlerTests
    {
        Mock<ITerritoryRepository> _territoryRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _territoryRepository = new Mock<ITerritoryRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Territory_GetQuery_Success()
        {
            //Arrange
            var query = new GetTerritoryQuery();

            _territoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Territory, bool>>>())).ReturnsAsync(new Territory()
//propertyler buraya yazılacak
//{																		
//TerritoryId = 1,
//TerritoryName = "Test"
//}
);

            var handler = new GetTerritoryQueryHandler(_territoryRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.TerritoryId.Should().Be(1);

        }

        [Test]
        public async Task Territory_GetQueries_Success()
        {
            //Arrange
            var query = new GetTerritoriesQuery();

            _territoryRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Territory, bool>>>()))
                        .ReturnsAsync(new List<Territory> { new Territory() { /*TODO:propertyler buraya yazılacak TerritoryId = 1, TerritoryName = "test"*/ } });

            var handler = new GetTerritoriesQueryHandler(_territoryRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Territory>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Territory_CreateCommand_Success()
        {
            Territory rt = null;
            //Arrange
            var command = new CreateTerritoryCommand();
            //propertyler buraya yazılacak
            //command.TerritoryName = "deneme";

            _territoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Territory, bool>>>()))
                        .ReturnsAsync(rt);

            _territoryRepository.Setup(x => x.Add(It.IsAny<Territory>())).Returns(new Territory());

            var handler = new CreateTerritoryCommandHandler(_territoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _territoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Territory_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateTerritoryCommand();
            //propertyler buraya yazılacak 
            //command.TerritoryName = "test";

            _territoryRepository.Setup(x => x.Query())
                                           .Returns(new List<Territory> { new Territory() { /*TODO:propertyler buraya yazılacak TerritoryId = 1, TerritoryName = "test"*/ } }.AsQueryable());

            _territoryRepository.Setup(x => x.Add(It.IsAny<Territory>())).Returns(new Territory());

            var handler = new CreateTerritoryCommandHandler(_territoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Territory_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateTerritoryCommand();
            //command.TerritoryName = "test";

            _territoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Territory, bool>>>()))
                        .ReturnsAsync(new Territory() { /*TODO:propertyler buraya yazılacak TerritoryId = 1, TerritoryName = "deneme"*/ });

            _territoryRepository.Setup(x => x.Update(It.IsAny<Territory>())).Returns(new Territory());

            var handler = new UpdateTerritoryCommandHandler(_territoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _territoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Territory_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteTerritoryCommand();

            _territoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Territory, bool>>>()))
                        .ReturnsAsync(new Territory() { /*TODO:propertyler buraya yazılacak TerritoryId = 1, TerritoryName = "deneme"*/});

            _territoryRepository.Setup(x => x.Delete(It.IsAny<Territory>()));

            var handler = new DeleteTerritoryCommandHandler(_territoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _territoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

