
using Business.Handlers.Regions.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Regions.Queries.GetRegionQuery;
using Entities.Concrete;
using static Business.Handlers.Regions.Queries.GetRegionsQuery;
using static Business.Handlers.Regions.Commands.CreateRegionCommand;
using Business.Handlers.Regions.Commands;
using Business.Constants;
using static Business.Handlers.Regions.Commands.UpdateRegionCommand;
using static Business.Handlers.Regions.Commands.DeleteRegionCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RegionHandlerTests
    {
        Mock<IRegionRepository> _regionRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _regionRepository = new Mock<IRegionRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Region_GetQuery_Success()
        {
            //Arrange
            var query = new GetRegionQuery();

            _regionRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Region, bool>>>())).ReturnsAsync(new Region()
//propertyler buraya yazılacak
//{																		
//RegionId = 1,
//RegionName = "Test"
//}
);

            var handler = new GetRegionQueryHandler(_regionRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RegionId.Should().Be(1);

        }

        [Test]
        public async Task Region_GetQueries_Success()
        {
            //Arrange
            var query = new GetRegionsQuery();

            _regionRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Region, bool>>>()))
                        .ReturnsAsync(new List<Region> { new Region() { /*TODO:propertyler buraya yazılacak RegionId = 1, RegionName = "test"*/ } });

            var handler = new GetRegionsQueryHandler(_regionRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Region>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Region_CreateCommand_Success()
        {
            Region rt = null;
            //Arrange
            var command = new CreateRegionCommand();
            //propertyler buraya yazılacak
            //command.RegionName = "deneme";

            _regionRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Region, bool>>>()))
                        .ReturnsAsync(rt);

            _regionRepository.Setup(x => x.Add(It.IsAny<Region>())).Returns(new Region());

            var handler = new CreateRegionCommandHandler(_regionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _regionRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Region_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRegionCommand();
            //propertyler buraya yazılacak 
            //command.RegionName = "test";

            _regionRepository.Setup(x => x.Query())
                                           .Returns(new List<Region> { new Region() { /*TODO:propertyler buraya yazılacak RegionId = 1, RegionName = "test"*/ } }.AsQueryable());

            _regionRepository.Setup(x => x.Add(It.IsAny<Region>())).Returns(new Region());

            var handler = new CreateRegionCommandHandler(_regionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Region_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRegionCommand();
            //command.RegionName = "test";

            _regionRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Region, bool>>>()))
                        .ReturnsAsync(new Region() { /*TODO:propertyler buraya yazılacak RegionId = 1, RegionName = "deneme"*/ });

            _regionRepository.Setup(x => x.Update(It.IsAny<Region>())).Returns(new Region());

            var handler = new UpdateRegionCommandHandler(_regionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _regionRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Region_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRegionCommand();

            _regionRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Region, bool>>>()))
                        .ReturnsAsync(new Region() { /*TODO:propertyler buraya yazılacak RegionId = 1, RegionName = "deneme"*/});

            _regionRepository.Setup(x => x.Delete(It.IsAny<Region>()));

            var handler = new DeleteRegionCommandHandler(_regionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _regionRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

