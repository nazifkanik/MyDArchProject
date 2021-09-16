
using Business.Handlers.EmployeeTerritories.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.EmployeeTerritories.Queries.GetEmployeeTerritoryQuery;
using Entities.Concrete;
using static Business.Handlers.EmployeeTerritories.Queries.GetEmployeeTerritoriesQuery;
using static Business.Handlers.EmployeeTerritories.Commands.CreateEmployeeTerritoryCommand;
using Business.Handlers.EmployeeTerritories.Commands;
using Business.Constants;
using static Business.Handlers.EmployeeTerritories.Commands.UpdateEmployeeTerritoryCommand;
using static Business.Handlers.EmployeeTerritories.Commands.DeleteEmployeeTerritoryCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EmployeeTerritoryHandlerTests
    {
        Mock<IEmployeeTerritoryRepository> _employeeTerritoryRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _employeeTerritoryRepository = new Mock<IEmployeeTerritoryRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task EmployeeTerritory_GetQuery_Success()
        {
            //Arrange
            var query = new GetEmployeeTerritoryQuery();

            _employeeTerritoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EmployeeTerritory, bool>>>())).ReturnsAsync(new EmployeeTerritory()
//propertyler buraya yazılacak
//{																		
//EmployeeTerritoryId = 1,
//EmployeeTerritoryName = "Test"
//}
);

            var handler = new GetEmployeeTerritoryQueryHandler(_employeeTerritoryRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.EmployeeTerritoryId.Should().Be(1);

        }

        [Test]
        public async Task EmployeeTerritory_GetQueries_Success()
        {
            //Arrange
            var query = new GetEmployeeTerritoriesQuery();

            _employeeTerritoryRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<EmployeeTerritory, bool>>>()))
                        .ReturnsAsync(new List<EmployeeTerritory> { new EmployeeTerritory() { /*TODO:propertyler buraya yazılacak EmployeeTerritoryId = 1, EmployeeTerritoryName = "test"*/ } });

            var handler = new GetEmployeeTerritoriesQueryHandler(_employeeTerritoryRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<EmployeeTerritory>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task EmployeeTerritory_CreateCommand_Success()
        {
            EmployeeTerritory rt = null;
            //Arrange
            var command = new CreateEmployeeTerritoryCommand();
            //propertyler buraya yazılacak
            //command.EmployeeTerritoryName = "deneme";

            _employeeTerritoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EmployeeTerritory, bool>>>()))
                        .ReturnsAsync(rt);

            _employeeTerritoryRepository.Setup(x => x.Add(It.IsAny<EmployeeTerritory>())).Returns(new EmployeeTerritory());

            var handler = new CreateEmployeeTerritoryCommandHandler(_employeeTerritoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _employeeTerritoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task EmployeeTerritory_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEmployeeTerritoryCommand();
            //propertyler buraya yazılacak 
            //command.EmployeeTerritoryName = "test";

            _employeeTerritoryRepository.Setup(x => x.Query())
                                           .Returns(new List<EmployeeTerritory> { new EmployeeTerritory() { /*TODO:propertyler buraya yazılacak EmployeeTerritoryId = 1, EmployeeTerritoryName = "test"*/ } }.AsQueryable());

            _employeeTerritoryRepository.Setup(x => x.Add(It.IsAny<EmployeeTerritory>())).Returns(new EmployeeTerritory());

            var handler = new CreateEmployeeTerritoryCommandHandler(_employeeTerritoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task EmployeeTerritory_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEmployeeTerritoryCommand();
            //command.EmployeeTerritoryName = "test";

            _employeeTerritoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EmployeeTerritory, bool>>>()))
                        .ReturnsAsync(new EmployeeTerritory() { /*TODO:propertyler buraya yazılacak EmployeeTerritoryId = 1, EmployeeTerritoryName = "deneme"*/ });

            _employeeTerritoryRepository.Setup(x => x.Update(It.IsAny<EmployeeTerritory>())).Returns(new EmployeeTerritory());

            var handler = new UpdateEmployeeTerritoryCommandHandler(_employeeTerritoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _employeeTerritoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task EmployeeTerritory_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEmployeeTerritoryCommand();

            _employeeTerritoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EmployeeTerritory, bool>>>()))
                        .ReturnsAsync(new EmployeeTerritory() { /*TODO:propertyler buraya yazılacak EmployeeTerritoryId = 1, EmployeeTerritoryName = "deneme"*/});

            _employeeTerritoryRepository.Setup(x => x.Delete(It.IsAny<EmployeeTerritory>()));

            var handler = new DeleteEmployeeTerritoryCommandHandler(_employeeTerritoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _employeeTerritoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

