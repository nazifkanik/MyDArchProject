
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
using Business.Handlers.OrderDetails.ValidationRules;

namespace Business.Handlers.OrderDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderDetailCommand : IRequest<IResult>
    {

        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }


        public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, IResult>
        {
            private readonly IOrderDetailRepository _orderDetailRepository;
            private readonly IMediator _mediator;
            public CreateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IMediator mediator)
            {
                _orderDetailRepository = orderDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrderDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
            {
                var isThereOrderDetailRecord = _orderDetailRepository.Query().Any(u => u.ProductId == request.ProductId);

                if (isThereOrderDetailRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrderDetail = new OrderDetail
                {
                    ProductId = request.ProductId,
                    UnitPrice = request.UnitPrice,
                    Quantity = request.Quantity,

                };

                _orderDetailRepository.Add(addedOrderDetail);
                await _orderDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}