
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


namespace Business.Handlers.Shippers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteShipperCommand : IRequest<IResult>
    {
        public int ShipperId { get; set; }

        public class DeleteShipperCommandHandler : IRequestHandler<DeleteShipperCommand, IResult>
        {
            private readonly IShipperRepository _shipperRepository;
            private readonly IMediator _mediator;

            public DeleteShipperCommandHandler(IShipperRepository shipperRepository, IMediator mediator)
            {
                _shipperRepository = shipperRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteShipperCommand request, CancellationToken cancellationToken)
            {
                var shipperToDelete = _shipperRepository.Get(p => p.ShipperId == request.ShipperId);

                _shipperRepository.Delete(shipperToDelete);
                await _shipperRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

