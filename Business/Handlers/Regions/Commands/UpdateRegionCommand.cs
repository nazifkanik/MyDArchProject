
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Regions.ValidationRules;


namespace Business.Handlers.Regions.Commands
{


    public class UpdateRegionCommand : IRequest<IResult>
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }

        public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, IResult>
        {
            private readonly IRegionRepository _regionRepository;
            private readonly IMediator _mediator;

            public UpdateRegionCommandHandler(IRegionRepository regionRepository, IMediator mediator)
            {
                _regionRepository = regionRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRegionValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
            {
                var isThereRegionRecord = await _regionRepository.GetAsync(u => u.RegionId == request.RegionId);


                isThereRegionRecord.RegionDescription = request.RegionDescription;


                _regionRepository.Update(isThereRegionRecord);
                await _regionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

