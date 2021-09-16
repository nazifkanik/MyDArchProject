
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
using Business.Handlers.Regions.ValidationRules;

namespace Business.Handlers.Regions.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRegionCommand : IRequest<IResult>
    {

        public string RegionDescription { get; set; }


        public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, IResult>
        {
            private readonly IRegionRepository _regionRepository;
            private readonly IMediator _mediator;
            public CreateRegionCommandHandler(IRegionRepository regionRepository, IMediator mediator)
            {
                _regionRepository = regionRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRegionValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
            {
                var isThereRegionRecord = _regionRepository.Query().Any(u => u.RegionDescription == request.RegionDescription);

                if (isThereRegionRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRegion = new Region
                {
                    RegionDescription = request.RegionDescription,

                };

                _regionRepository.Add(addedRegion);
                await _regionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}