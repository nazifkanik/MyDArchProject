
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Territories.ValidationRules;

namespace Business.Handlers.Territories.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateTerritoryCommand : IRequest<IResult>
    {

        public int RegionId { get; set; }
        public string TerritoryDescription { get; set; }


        public class CreateTerritoryCommandHandler : IRequestHandler<CreateTerritoryCommand, IResult>
        {
            private readonly ITerritoryRepository _territoryRepository;
            private readonly IMediator _mediator;
            public CreateTerritoryCommandHandler(ITerritoryRepository territoryRepository, IMediator mediator)
            {
                _territoryRepository = territoryRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateTerritoryValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateTerritoryCommand request, CancellationToken cancellationToken)
            {
                var isThereTerritoryRecord = _territoryRepository.Query().Any(u => u.RegionId == request.RegionId);

                if (isThereTerritoryRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedTerritory = new Territory
                {
                    RegionId = request.RegionId,
                    TerritoryDescription = request.TerritoryDescription,

                };

                _territoryRepository.Add(addedTerritory);
                await _territoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}