
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Regions.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRegionCommand : IRequest<IResult>
    {
        public int RegionId { get; set; }

        public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand, IResult>
        {
            private readonly IRegionRepository _regionRepository;
            private readonly IMediator _mediator;

            public DeleteRegionCommandHandler(IRegionRepository regionRepository, IMediator mediator)
            {
                _regionRepository = regionRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
            {
                var regionToDelete = _regionRepository.Get(p => p.RegionId == request.RegionId);

                _regionRepository.Delete(regionToDelete);
                await _regionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

