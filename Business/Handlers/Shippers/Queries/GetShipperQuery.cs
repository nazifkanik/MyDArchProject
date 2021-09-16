
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Shippers.Queries
{
    public class GetShipperQuery : IRequest<IDataResult<Shipper>>
    {
        public int ShipperId { get; set; }

        public class GetShipperQueryHandler : IRequestHandler<GetShipperQuery, IDataResult<Shipper>>
        {
            private readonly IShipperRepository _shipperRepository;
            private readonly IMediator _mediator;

            public GetShipperQueryHandler(IShipperRepository shipperRepository, IMediator mediator)
            {
                _shipperRepository = shipperRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Shipper>> Handle(GetShipperQuery request, CancellationToken cancellationToken)
            {
                var shipper = await _shipperRepository.GetAsync(p => p.ShipperId == request.ShipperId);
                return new SuccessDataResult<Shipper>(shipper);
            }
        }
    }
}
