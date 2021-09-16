
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
using Business.Handlers.Territories.ValidationRules;


namespace Business.Handlers.Territories.Commands
{


    public class UpdateTerritoryCommand : IRequest<IResult>
    {
        public string TerritoryId { get; set; }
        public int RegionId { get; set; }
        public string TerritoryDescription { get; set; }

        public class UpdateTerritoryCommandHandler : IRequestHandler<UpdateTerritoryCommand, IResult>
        {
            private readonly ITerritoryRepository _territoryRepository;
            private readonly IMediator _mediator;

            public UpdateTerritoryCommandHandler(ITerritoryRepository territoryRepository, IMediator mediator)
            {
                _territoryRepository = territoryRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateTerritoryValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateTerritoryCommand request, CancellationToken cancellationToken)
            {
                var isThereTerritoryRecord = await _territoryRepository.GetAsync(u => u.TerritoryId == request.TerritoryId);


                isThereTerritoryRecord.RegionId = request.RegionId;
                isThereTerritoryRecord.TerritoryDescription = request.TerritoryDescription;


                _territoryRepository.Update(isThereTerritoryRecord);
                await _territoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

