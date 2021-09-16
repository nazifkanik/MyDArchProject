
using Business.Handlers.Categories.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Categories.Queries.GetCategoryQuery;
using Entities.Concrete;
using static Business.Handlers.Categories.Queries.GetCategoriesQuery;
using static Business.Handlers.Categories.Commands.CreateCategoryCommand;
using Business.Handlers.Categories.Commands;
using Business.Constants;
using static Business.Handlers.Categories.Commands.UpdateCategoryCommand;
using static Business.Handlers.Categories.Commands.DeleteCategoryCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CategoryHandlerTests
    {
        Mock<ICategoryRepository> _categoryRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Category_GetQuery_Success()
        {
            //Arrange
            var query = new GetCategoryQuery();

            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(new Category()
//propertyler buraya yazılacak
//{																		
//CategoryId = 1,
//CategoryName = "Test"
//}
);

            var handler = new GetCategoryQueryHandler(_categoryRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CategoryId.Should().Be(1);

        }

        [Test]
        public async Task Category_GetQueries_Success()
        {
            //Arrange
            var query = new GetCategoriesQuery();

            _categoryRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                        .ReturnsAsync(new List<Category> { new Category() { /*TODO:propertyler buraya yazılacak CategoryId = 1, CategoryName = "test"*/ } });

            var handler = new GetCategoriesQueryHandler(_categoryRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Category>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Category_CreateCommand_Success()
        {
            Category rt = null;
            //Arrange
            var command = new CreateCategoryCommand();
            //propertyler buraya yazılacak
            //command.CategoryName = "deneme";

            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                        .ReturnsAsync(rt);

            _categoryRepository.Setup(x => x.Add(It.IsAny<Category>())).Returns(new Category());

            var handler = new CreateCategoryCommandHandler(_categoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _categoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Category_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCategoryCommand();
            //propertyler buraya yazılacak 
            //command.CategoryName = "test";

            _categoryRepository.Setup(x => x.Query())
                                           .Returns(new List<Category> { new Category() { /*TODO:propertyler buraya yazılacak CategoryId = 1, CategoryName = "test"*/ } }.AsQueryable());

            _categoryRepository.Setup(x => x.Add(It.IsAny<Category>())).Returns(new Category());

            var handler = new CreateCategoryCommandHandler(_categoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Category_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCategoryCommand();
            //command.CategoryName = "test";

            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                        .ReturnsAsync(new Category() { /*TODO:propertyler buraya yazılacak CategoryId = 1, CategoryName = "deneme"*/ });

            _categoryRepository.Setup(x => x.Update(It.IsAny<Category>())).Returns(new Category());

            var handler = new UpdateCategoryCommandHandler(_categoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _categoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Category_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCategoryCommand();

            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                        .ReturnsAsync(new Category() { /*TODO:propertyler buraya yazılacak CategoryId = 1, CategoryName = "deneme"*/});

            _categoryRepository.Setup(x => x.Delete(It.IsAny<Category>()));

            var handler = new DeleteCategoryCommandHandler(_categoryRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _categoryRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

