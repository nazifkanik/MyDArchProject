
using Business.Handlers.Shippers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Shippers.Queries.GetShipperQuery;
using Entities.Concrete;
using static Business.Handlers.Shippers.Queries.GetShippersQuery;
using static Business.Handlers.Shippers.Commands.CreateShipperCommand;
using Business.Handlers.Shippers.Commands;
using Business.Constants;
using static Business.Handlers.Shippers.Commands.UpdateShipperCommand;
using static Business.Handlers.Shippers.Commands.DeleteShipperCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ShipperHandlerTests
    {
        Mock<IShipperRepository> _shipperRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _shipperRepository = new Mock<IShipperRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Shipper_GetQuery_Success()
        {
            //Arrange
            var query = new GetShipperQuery();

            _shipperRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Shipper, bool>>>())).ReturnsAsync(new Shipper()
//propertyler buraya yazılacak
//{																		
//ShipperId = 1,
//ShipperName = "Test"
//}
);

            var handler = new GetShipperQueryHandler(_shipperRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ShipperId.Should().Be(1);

        }

        [Test]
        public async Task Shipper_GetQueries_Success()
        {
            //Arrange
            var query = new GetShippersQuery();

            _shipperRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Shipper, bool>>>()))
                        .ReturnsAsync(new List<Shipper> { new Shipper() { /*TODO:propertyler buraya yazılacak ShipperId = 1, ShipperName = "test"*/ } });

            var handler = new GetShippersQueryHandler(_shipperRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Shipper>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Shipper_CreateCommand_Success()
        {
            Shipper rt = null;
            //Arrange
            var command = new CreateShipperCommand();
            //propertyler buraya yazılacak
            //command.ShipperName = "deneme";

            _shipperRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Shipper, bool>>>()))
                        .ReturnsAsync(rt);

            _shipperRepository.Setup(x => x.Add(It.IsAny<Shipper>())).Returns(new Shipper());

            var handler = new CreateShipperCommandHandler(_shipperRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _shipperRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Shipper_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateShipperCommand();
            //propertyler buraya yazılacak 
            //command.ShipperName = "test";

            _shipperRepository.Setup(x => x.Query())
                                           .Returns(new List<Shipper> { new Shipper() { /*TODO:propertyler buraya yazılacak ShipperId = 1, ShipperName = "test"*/ } }.AsQueryable());

            _shipperRepository.Setup(x => x.Add(It.IsAny<Shipper>())).Returns(new Shipper());

            var handler = new CreateShipperCommandHandler(_shipperRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Shipper_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateShipperCommand();
            //command.ShipperName = "test";

            _shipperRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Shipper, bool>>>()))
                        .ReturnsAsync(new Shipper() { /*TODO:propertyler buraya yazılacak ShipperId = 1, ShipperName = "deneme"*/ });

            _shipperRepository.Setup(x => x.Update(It.IsAny<Shipper>())).Returns(new Shipper());

            var handler = new UpdateShipperCommandHandler(_shipperRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _shipperRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Shipper_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteShipperCommand();

            _shipperRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Shipper, bool>>>()))
                        .ReturnsAsync(new Shipper() { /*TODO:propertyler buraya yazılacak ShipperId = 1, ShipperName = "deneme"*/});

            _shipperRepository.Setup(x => x.Delete(It.IsAny<Shipper>()));

            var handler = new DeleteShipperCommandHandler(_shipperRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _shipperRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

