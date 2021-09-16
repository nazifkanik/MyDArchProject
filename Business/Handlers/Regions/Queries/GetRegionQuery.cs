
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Regions.Queries
{
    public class GetRegionQuery : IRequest<IDataResult<Region>>
    {
        public int RegionId { get; set; }

        public class GetRegionQueryHandler : IRequestHandler<GetRegionQuery, IDataResult<Region>>
        {
            private readonly IRegionRepository _regionRepository;
            private readonly IMediator _mediator;

            public GetRegionQueryHandler(IRegionRepository regionRepository, IMediator mediator)
            {
                _regionRepository = regionRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Region>> Handle(GetRegionQuery request, CancellationToken cancellationToken)
            {
                var region = await _regionRepository.GetAsync(p => p.RegionId == request.RegionId);
                return new SuccessDataResult<Region>(region);
            }
        }
    }
}
