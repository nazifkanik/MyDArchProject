
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


namespace Business.Handlers.OrderDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrderDetailCommand : IRequest<IResult>
    {
        public int OrderId { get; set; }

        public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, IResult>
        {
            private readonly IOrderDetailRepository _orderDetailRepository;
            private readonly IMediator _mediator;

            public DeleteOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IMediator mediator)
            {
                _orderDetailRepository = orderDetailRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
            {
                var orderDetailToDelete = _orderDetailRepository.Get(p => p.OrderId == request.OrderId);

                _orderDetailRepository.Delete(orderDetailToDelete);
                await _orderDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

