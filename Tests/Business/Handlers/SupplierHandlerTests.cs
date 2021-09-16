
using Business.Handlers.Suppliers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Suppliers.Queries.GetSupplierQuery;
using Entities.Concrete;
using static Business.Handlers.Suppliers.Queries.GetSuppliersQuery;
using static Business.Handlers.Suppliers.Commands.CreateSupplierCommand;
using Business.Handlers.Suppliers.Commands;
using Business.Constants;
using static Business.Handlers.Suppliers.Commands.UpdateSupplierCommand;
using static Business.Handlers.Suppliers.Commands.DeleteSupplierCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SupplierHandlerTests
    {
        Mock<ISupplierRepository> _supplierRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _supplierRepository = new Mock<ISupplierRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Supplier_GetQuery_Success()
        {
            //Arrange
            var query = new GetSupplierQuery();

            _supplierRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Supplier, bool>>>())).ReturnsAsync(new Supplier()
//propertyler buraya yazılacak
//{																		
//SupplierId = 1,
//SupplierName = "Test"
//}
);

            var handler = new GetSupplierQueryHandler(_supplierRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SupplierId.Should().Be(1);

        }

        [Test]
        public async Task Supplier_GetQueries_Success()
        {
            //Arrange
            var query = new GetSuppliersQuery();

            _supplierRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                        .ReturnsAsync(new List<Supplier> { new Supplier() { /*TODO:propertyler buraya yazılacak SupplierId = 1, SupplierName = "test"*/ } });

            var handler = new GetSuppliersQueryHandler(_supplierRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Supplier>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Supplier_CreateCommand_Success()
        {
            Supplier rt = null;
            //Arrange
            var command = new CreateSupplierCommand();
            //propertyler buraya yazılacak
            //command.SupplierName = "deneme";

            _supplierRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                        .ReturnsAsync(rt);

            _supplierRepository.Setup(x => x.Add(It.IsAny<Supplier>())).Returns(new Supplier());

            var handler = new CreateSupplierCommandHandler(_supplierRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _supplierRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Supplier_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSupplierCommand();
            //propertyler buraya yazılacak 
            //command.SupplierName = "test";

            _supplierRepository.Setup(x => x.Query())
                                           .Returns(new List<Supplier> { new Supplier() { /*TODO:propertyler buraya yazılacak SupplierId = 1, SupplierName = "test"*/ } }.AsQueryable());

            _supplierRepository.Setup(x => x.Add(It.IsAny<Supplier>())).Returns(new Supplier());

            var handler = new CreateSupplierCommandHandler(_supplierRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Supplier_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSupplierCommand();
            //command.SupplierName = "test";

            _supplierRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                        .ReturnsAsync(new Supplier() { /*TODO:propertyler buraya yazılacak SupplierId = 1, SupplierName = "deneme"*/ });

            _supplierRepository.Setup(x => x.Update(It.IsAny<Supplier>())).Returns(new Supplier());

            var handler = new UpdateSupplierCommandHandler(_supplierRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _supplierRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Supplier_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSupplierCommand();

            _supplierRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                        .ReturnsAsync(new Supplier() { /*TODO:propertyler buraya yazılacak SupplierId = 1, SupplierName = "deneme"*/});

            _supplierRepository.Setup(x => x.Delete(It.IsAny<Supplier>()));

            var handler = new DeleteSupplierCommandHandler(_supplierRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _supplierRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

