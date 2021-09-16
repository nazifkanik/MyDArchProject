
using Business.Handlers.Employees.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Employees.Queries.GetEmployeeQuery;
using Entities.Concrete;
using static Business.Handlers.Employees.Queries.GetEmployeesQuery;
using static Business.Handlers.Employees.Commands.CreateEmployeeCommand;
using Business.Handlers.Employees.Commands;
using Business.Constants;
using static Business.Handlers.Employees.Commands.UpdateEmployeeCommand;
using static Business.Handlers.Employees.Commands.DeleteEmployeeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EmployeeHandlerTests
    {
        Mock<IEmployeeRepository> _employeeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Employee_GetQuery_Success()
        {
            //Arrange
            var query = new GetEmployeeQuery();

            _employeeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>())).ReturnsAsync(new Employee()
//propertyler buraya yazılacak
//{																		
//EmployeeId = 1,
//EmployeeName = "Test"
//}
);

            var handler = new GetEmployeeQueryHandler(_employeeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.EmployeeId.Should().Be(1);

        }

        [Test]
        public async Task Employee_GetQueries_Success()
        {
            //Arrange
            var query = new GetEmployeesQuery();

            _employeeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                        .ReturnsAsync(new List<Employee> { new Employee() { /*TODO:propertyler buraya yazılacak EmployeeId = 1, EmployeeName = "test"*/ } });

            var handler = new GetEmployeesQueryHandler(_employeeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Employee>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Employee_CreateCommand_Success()
        {
            Employee rt = null;
            //Arrange
            var command = new CreateEmployeeCommand();
            //propertyler buraya yazılacak
            //command.EmployeeName = "deneme";

            _employeeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                        .ReturnsAsync(rt);

            _employeeRepository.Setup(x => x.Add(It.IsAny<Employee>())).Returns(new Employee());

            var handler = new CreateEmployeeCommandHandler(_employeeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _employeeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Employee_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEmployeeCommand();
            //propertyler buraya yazılacak 
            //command.EmployeeName = "test";

            _employeeRepository.Setup(x => x.Query())
                                           .Returns(new List<Employee> { new Employee() { /*TODO:propertyler buraya yazılacak EmployeeId = 1, EmployeeName = "test"*/ } }.AsQueryable());

            _employeeRepository.Setup(x => x.Add(It.IsAny<Employee>())).Returns(new Employee());

            var handler = new CreateEmployeeCommandHandler(_employeeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Employee_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEmployeeCommand();
            //command.EmployeeName = "test";

            _employeeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                        .ReturnsAsync(new Employee() { /*TODO:propertyler buraya yazılacak EmployeeId = 1, EmployeeName = "deneme"*/ });

            _employeeRepository.Setup(x => x.Update(It.IsAny<Employee>())).Returns(new Employee());

            var handler = new UpdateEmployeeCommandHandler(_employeeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _employeeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Employee_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEmployeeCommand();

            _employeeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                        .ReturnsAsync(new Employee() { /*TODO:propertyler buraya yazılacak EmployeeId = 1, EmployeeName = "deneme"*/});

            _employeeRepository.Setup(x => x.Delete(It.IsAny<Employee>()));

            var handler = new DeleteEmployeeCommandHandler(_employeeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _employeeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

