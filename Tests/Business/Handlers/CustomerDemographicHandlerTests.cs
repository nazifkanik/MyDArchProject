
using Business.Handlers.CustomerDemographics.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.CustomerDemographics.Queries.GetCustomerDemographicQuery;
using Entities.Concrete;
using static Business.Handlers.CustomerDemographics.Queries.GetCustomerDemographicsQuery;
using static Business.Handlers.CustomerDemographics.Commands.CreateCustomerDemographicCommand;
using Business.Handlers.CustomerDemographics.Commands;
using Business.Constants;
using static Business.Handlers.CustomerDemographics.Commands.UpdateCustomerDemographicCommand;
using static Business.Handlers.CustomerDemographics.Commands.DeleteCustomerDemographicCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CustomerDemographicHandlerTests
    {
        Mock<ICustomerDemographicRepository> _customerDemographicRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _customerDemographicRepository = new Mock<ICustomerDemographicRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task CustomerDemographic_GetQuery_Success()
        {
            //Arrange
            var query = new GetCustomerDemographicQuery();

            _customerDemographicRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerDemographic, bool>>>())).ReturnsAsync(new CustomerDemographic()
//propertyler buraya yazılacak
//{																		
//CustomerDemographicId = 1,
//CustomerDemographicName = "Test"
//}
);

            var handler = new GetCustomerDemographicQueryHandler(_customerDemographicRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CustomerDemographicId.Should().Be(1);

        }

        [Test]
        public async Task CustomerDemographic_GetQueries_Success()
        {
            //Arrange
            var query = new GetCustomerDemographicsQuery();

            _customerDemographicRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<CustomerDemographic, bool>>>()))
                        .ReturnsAsync(new List<CustomerDemographic> { new CustomerDemographic() { /*TODO:propertyler buraya yazılacak CustomerDemographicId = 1, CustomerDemographicName = "test"*/ } });

            var handler = new GetCustomerDemographicsQueryHandler(_customerDemographicRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<CustomerDemographic>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task CustomerDemographic_CreateCommand_Success()
        {
            CustomerDemographic rt = null;
            //Arrange
            var command = new CreateCustomerDemographicCommand();
            //propertyler buraya yazılacak
            //command.CustomerDemographicName = "deneme";

            _customerDemographicRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerDemographic, bool>>>()))
                        .ReturnsAsync(rt);

            _customerDemographicRepository.Setup(x => x.Add(It.IsAny<CustomerDemographic>())).Returns(new CustomerDemographic());

            var handler = new CreateCustomerDemographicCommandHandler(_customerDemographicRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerDemographicRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task CustomerDemographic_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCustomerDemographicCommand();
            //propertyler buraya yazılacak 
            //command.CustomerDemographicName = "test";

            _customerDemographicRepository.Setup(x => x.Query())
                                           .Returns(new List<CustomerDemographic> { new CustomerDemographic() { /*TODO:propertyler buraya yazılacak CustomerDemographicId = 1, CustomerDemographicName = "test"*/ } }.AsQueryable());

            _customerDemographicRepository.Setup(x => x.Add(It.IsAny<CustomerDemographic>())).Returns(new CustomerDemographic());

            var handler = new CreateCustomerDemographicCommandHandler(_customerDemographicRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task CustomerDemographic_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCustomerDemographicCommand();
            //command.CustomerDemographicName = "test";

            _customerDemographicRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerDemographic, bool>>>()))
                        .ReturnsAsync(new CustomerDemographic() { /*TODO:propertyler buraya yazılacak CustomerDemographicId = 1, CustomerDemographicName = "deneme"*/ });

            _customerDemographicRepository.Setup(x => x.Update(It.IsAny<CustomerDemographic>())).Returns(new CustomerDemographic());

            var handler = new UpdateCustomerDemographicCommandHandler(_customerDemographicRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerDemographicRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task CustomerDemographic_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCustomerDemographicCommand();

            _customerDemographicRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerDemographic, bool>>>()))
                        .ReturnsAsync(new CustomerDemographic() { /*TODO:propertyler buraya yazılacak CustomerDemographicId = 1, CustomerDemographicName = "deneme"*/});

            _customerDemographicRepository.Setup(x => x.Delete(It.IsAny<CustomerDemographic>()));

            var handler = new DeleteCustomerDemographicCommandHandler(_customerDemographicRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerDemographicRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

