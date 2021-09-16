
using Business.Handlers.OrderDetails.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrderDetails.Queries.GetOrderDetailQuery;
using Entities.Concrete;
using static Business.Handlers.OrderDetails.Queries.GetOrderDetailsQuery;
using static Business.Handlers.OrderDetails.Commands.CreateOrderDetailCommand;
using Business.Handlers.OrderDetails.Commands;
using Business.Constants;
using static Business.Handlers.OrderDetails.Commands.UpdateOrderDetailCommand;
using static Business.Handlers.OrderDetails.Commands.DeleteOrderDetailCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrderDetailHandlerTests
    {
        Mock<IOrderDetailRepository> _orderDetailRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orderDetailRepository = new Mock<IOrderDetailRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrderDetail_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrderDetailQuery();

            _orderDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderDetail, bool>>>())).ReturnsAsync(new OrderDetail()
//propertyler buraya yazılacak
//{																		
//OrderDetailId = 1,
//OrderDetailName = "Test"
//}
);

            var handler = new GetOrderDetailQueryHandler(_orderDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrderDetailId.Should().Be(1);

        }

        [Test]
        public async Task OrderDetail_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrderDetailsQuery();

            _orderDetailRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrderDetail, bool>>>()))
                        .ReturnsAsync(new List<OrderDetail> { new OrderDetail() { /*TODO:propertyler buraya yazılacak OrderDetailId = 1, OrderDetailName = "test"*/ } });

            var handler = new GetOrderDetailsQueryHandler(_orderDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrderDetail>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrderDetail_CreateCommand_Success()
        {
            OrderDetail rt = null;
            //Arrange
            var command = new CreateOrderDetailCommand();
            //propertyler buraya yazılacak
            //command.OrderDetailName = "deneme";

            _orderDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderDetail, bool>>>()))
                        .ReturnsAsync(rt);

            _orderDetailRepository.Setup(x => x.Add(It.IsAny<OrderDetail>())).Returns(new OrderDetail());

            var handler = new CreateOrderDetailCommandHandler(_orderDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrderDetail_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrderDetailCommand();
            //propertyler buraya yazılacak 
            //command.OrderDetailName = "test";

            _orderDetailRepository.Setup(x => x.Query())
                                           .Returns(new List<OrderDetail> { new OrderDetail() { /*TODO:propertyler buraya yazılacak OrderDetailId = 1, OrderDetailName = "test"*/ } }.AsQueryable());

            _orderDetailRepository.Setup(x => x.Add(It.IsAny<OrderDetail>())).Returns(new OrderDetail());

            var handler = new CreateOrderDetailCommandHandler(_orderDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrderDetail_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrderDetailCommand();
            //command.OrderDetailName = "test";

            _orderDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderDetail, bool>>>()))
                        .ReturnsAsync(new OrderDetail() { /*TODO:propertyler buraya yazılacak OrderDetailId = 1, OrderDetailName = "deneme"*/ });

            _orderDetailRepository.Setup(x => x.Update(It.IsAny<OrderDetail>())).Returns(new OrderDetail());

            var handler = new UpdateOrderDetailCommandHandler(_orderDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrderDetail_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrderDetailCommand();

            _orderDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderDetail, bool>>>()))
                        .ReturnsAsync(new OrderDetail() { /*TODO:propertyler buraya yazılacak OrderDetailId = 1, OrderDetailName = "deneme"*/});

            _orderDetailRepository.Setup(x => x.Delete(It.IsAny<OrderDetail>()));

            var handler = new DeleteOrderDetailCommandHandler(_orderDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

