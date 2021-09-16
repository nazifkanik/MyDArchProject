
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

namespace Business.Handlers.OrderDetails.Queries
{

    public class GetOrderDetailsQuery : IRequest<IDataResult<IEnumerable<OrderDetail>>>
    {
        public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, IDataResult<IEnumerable<OrderDetail>>>
        {
            private readonly IOrderDetailRepository _orderDetailRepository;
            private readonly IMediator _mediator;

            public GetOrderDetailsQueryHandler(IOrderDetailRepository orderDetailRepository, IMediator mediator)
            {
                _orderDetailRepository = orderDetailRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrderDetail>>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrderDetail>>(await _orderDetailRepository.GetListAsync());
            }
        }
    }
}