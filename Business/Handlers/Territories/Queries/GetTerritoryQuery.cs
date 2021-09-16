
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Territories.Queries
{
    public class GetTerritoryQuery : IRequest<IDataResult<Territory>>
    {
        public string TerritoryId { get; set; }

        public class GetTerritoryQueryHandler : IRequestHandler<GetTerritoryQuery, IDataResult<Territory>>
        {
            private readonly ITerritoryRepository _territoryRepository;
            private readonly IMediator _mediator;

            public GetTerritoryQueryHandler(ITerritoryRepository territoryRepository, IMediator mediator)
            {
                _territoryRepository = territoryRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Territory>> Handle(GetTerritoryQuery request, CancellationToken cancellationToken)
            {
                var territory = await _territoryRepository.GetAsync(p => p.TerritoryId == request.TerritoryId);
                return new SuccessDataResult<Territory>(territory);
            }
        }
    }
}
