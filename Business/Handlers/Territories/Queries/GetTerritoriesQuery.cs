
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Territories.Queries
{

    public class GetTerritoriesQuery : IRequest<IDataResult<IEnumerable<Territory>>>
    {
        public class GetTerritoriesQueryHandler : IRequestHandler<GetTerritoriesQuery, IDataResult<IEnumerable<Territory>>>
        {
            private readonly ITerritoryRepository _territoryRepository;
            private readonly IMediator _mediator;

            public GetTerritoriesQueryHandler(ITerritoryRepository territoryRepository, IMediator mediator)
            {
                _territoryRepository = territoryRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Territory>>> Handle(GetTerritoriesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Territory>>(await _territoryRepository.GetListAsync());
            }
        }
    }
}