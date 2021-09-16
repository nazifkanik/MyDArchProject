
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrderDetails.Queries
{
    public class GetOrderDetailQuery : IRequest<IDataResult<OrderDetail>>
    {
        public int OrderId { get; set; }

        public class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQuery, IDataResult<OrderDetail>>
        {
            private readonly IOrderDetailRepository _orderDetailRepository;
            private readonly IMediator _mediator;

            public GetOrderDetailQueryHandler(IOrderDetailRepository orderDetailRepository, IMediator mediator)
            {
                _orderDetailRepository = orderDetailRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrderDetail>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
            {
                var orderDetail = await _orderDetailRepository.GetAsync(p => p.OrderId == request.OrderId);
                return new SuccessDataResult<OrderDetail>(orderDetail);
            }
        }
    }
}
