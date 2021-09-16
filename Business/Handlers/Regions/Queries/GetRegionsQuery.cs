
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

namespace Business.Handlers.Regions.Queries
{

    public class GetRegionsQuery : IRequest<IDataResult<IEnumerable<Region>>>
    {
        public class GetRegionsQueryHandler : IRequestHandler<GetRegionsQuery, IDataResult<IEnumerable<Region>>>
        {
            private readonly IRegionRepository _regionRepository;
            private readonly IMediator _mediator;

            public GetRegionsQueryHandler(IRegionRepository regionRepository, IMediator mediator)
            {
                _regionRepository = regionRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Region>>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Region>>(await _regionRepository.GetListAsync());
            }
        }
    }
}