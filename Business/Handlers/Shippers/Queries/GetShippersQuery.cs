
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

namespace Business.Handlers.Shippers.Queries
{

    public class GetShippersQuery : IRequest<IDataResult<IEnumerable<Shipper>>>
    {
        public class GetShippersQueryHandler : IRequestHandler<GetShippersQuery, IDataResult<IEnumerable<Shipper>>>
        {
            private readonly IShipperRepository _shipperRepository;
            private readonly IMediator _mediator;

            public GetShippersQueryHandler(IShipperRepository shipperRepository, IMediator mediator)
            {
                _shipperRepository = shipperRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Shipper>>> Handle(GetShippersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Shipper>>(await _shipperRepository.GetListAsync());
            }
        }
    }
}