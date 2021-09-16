
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


namespace Business.Handlers.Territories.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteTerritoryCommand : IRequest<IResult>
    {
        public string TerritoryId { get; set; }

        public class DeleteTerritoryCommandHandler : IRequestHandler<DeleteTerritoryCommand, IResult>
        {
            private readonly ITerritoryRepository _territoryRepository;
            private readonly IMediator _mediator;

            public DeleteTerritoryCommandHandler(ITerritoryRepository territoryRepository, IMediator mediator)
            {
                _territoryRepository = territoryRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteTerritoryCommand request, CancellationToken cancellationToken)
            {
                var territoryToDelete = _territoryRepository.Get(p => p.TerritoryId == request.TerritoryId);

                _territoryRepository.Delete(territoryToDelete);
                await _territoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

