
using Business.Handlers.CustomerCustomerDemoes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.CustomerCustomerDemoes.Queries.GetCustomerCustomerDemoQuery;
using Entities.Concrete;
using static Business.Handlers.CustomerCustomerDemoes.Queries.GetCustomerCustomerDemoesQuery;
using static Business.Handlers.CustomerCustomerDemoes.Commands.CreateCustomerCustomerDemoCommand;
using Business.Handlers.CustomerCustomerDemoes.Commands;
using Business.Constants;
using static Business.Handlers.CustomerCustomerDemoes.Commands.UpdateCustomerCustomerDemoCommand;
using static Business.Handlers.CustomerCustomerDemoes.Commands.DeleteCustomerCustomerDemoCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CustomerCustomerDemoHandlerTests
    {
        Mock<ICustomerCustomerDemoRepository> _customerCustomerDemoRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _customerCustomerDemoRepository = new Mock<ICustomerCustomerDemoRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task CustomerCustomerDemo_GetQuery_Success()
        {
            //Arrange
            var query = new GetCustomerCustomerDemoQuery();

            _customerCustomerDemoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerCustomerDemo, bool>>>())).ReturnsAsync(new CustomerCustomerDemo()
//propertyler buraya yazılacak
//{																		
//CustomerCustomerDemoId = 1,
//CustomerCustomerDemoName = "Test"
//}
);

            var handler = new GetCustomerCustomerDemoQueryHandler(_customerCustomerDemoRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CustomerCustomerDemoId.Should().Be(1);

        }

        [Test]
        public async Task CustomerCustomerDemo_GetQueries_Success()
        {
            //Arrange
            var query = new GetCustomerCustomerDemoesQuery();

            _customerCustomerDemoRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<CustomerCustomerDemo, bool>>>()))
                        .ReturnsAsync(new List<CustomerCustomerDemo> { new CustomerCustomerDemo() { /*TODO:propertyler buraya yazılacak CustomerCustomerDemoId = 1, CustomerCustomerDemoName = "test"*/ } });

            var handler = new GetCustomerCustomerDemoesQueryHandler(_customerCustomerDemoRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<CustomerCustomerDemo>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task CustomerCustomerDemo_CreateCommand_Success()
        {
            CustomerCustomerDemo rt = null;
            //Arrange
            var command = new CreateCustomerCustomerDemoCommand();
            //propertyler buraya yazılacak
            //command.CustomerCustomerDemoName = "deneme";

            _customerCustomerDemoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerCustomerDemo, bool>>>()))
                        .ReturnsAsync(rt);

            _customerCustomerDemoRepository.Setup(x => x.Add(It.IsAny<CustomerCustomerDemo>())).Returns(new CustomerCustomerDemo());

            var handler = new CreateCustomerCustomerDemoCommandHandler(_customerCustomerDemoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerCustomerDemoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task CustomerCustomerDemo_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCustomerCustomerDemoCommand();
            //propertyler buraya yazılacak 
            //command.CustomerCustomerDemoName = "test";

            _customerCustomerDemoRepository.Setup(x => x.Query())
                                           .Returns(new List<CustomerCustomerDemo> { new CustomerCustomerDemo() { /*TODO:propertyler buraya yazılacak CustomerCustomerDemoId = 1, CustomerCustomerDemoName = "test"*/ } }.AsQueryable());

            _customerCustomerDemoRepository.Setup(x => x.Add(It.IsAny<CustomerCustomerDemo>())).Returns(new CustomerCustomerDemo());

            var handler = new CreateCustomerCustomerDemoCommandHandler(_customerCustomerDemoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task CustomerCustomerDemo_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCustomerCustomerDemoCommand();
            //command.CustomerCustomerDemoName = "test";

            _customerCustomerDemoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerCustomerDemo, bool>>>()))
                        .ReturnsAsync(new CustomerCustomerDemo() { /*TODO:propertyler buraya yazılacak CustomerCustomerDemoId = 1, CustomerCustomerDemoName = "deneme"*/ });

            _customerCustomerDemoRepository.Setup(x => x.Update(It.IsAny<CustomerCustomerDemo>())).Returns(new CustomerCustomerDemo());

            var handler = new UpdateCustomerCustomerDemoCommandHandler(_customerCustomerDemoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerCustomerDemoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task CustomerCustomerDemo_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCustomerCustomerDemoCommand();

            _customerCustomerDemoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerCustomerDemo, bool>>>()))
                        .ReturnsAsync(new CustomerCustomerDemo() { /*TODO:propertyler buraya yazılacak CustomerCustomerDemoId = 1, CustomerCustomerDemoName = "deneme"*/});

            _customerCustomerDemoRepository.Setup(x => x.Delete(It.IsAny<CustomerCustomerDemo>()));

            var handler = new DeleteCustomerCustomerDemoCommandHandler(_customerCustomerDemoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerCustomerDemoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

