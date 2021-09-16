
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
using Business.Handlers.Orders.ValidationRules;


namespace Business.Handlers.Orders.Commands
{


    public class UpdateOrderCommand : IRequest<IResult>
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int ShipperId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime RequiredDate { get; set; }
        public System.DateTime ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, IResult>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMediator _mediator;

            public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
            {
                _orderRepository = orderRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrderValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var isThereOrderRecord = await _orderRepository.GetAsync(u => u.OrderId == request.OrderId);


                isThereOrderRecord.CustomerId = request.CustomerId;
                isThereOrderRecord.EmployeeId = request.EmployeeId;
                isThereOrderRecord.ShipperId = request.ShipperId;
                isThereOrderRecord.OrderDate = request.OrderDate;
                isThereOrderRecord.RequiredDate = request.RequiredDate;
                isThereOrderRecord.ShippedDate = request.ShippedDate;
                isThereOrderRecord.Freight = request.Freight;
                isThereOrderRecord.ShipName = request.ShipName;
                isThereOrderRecord.ShipAddress = request.ShipAddress;
                isThereOrderRecord.ShipCity = request.ShipCity;
                isThereOrderRecord.ShipRegion = request.ShipRegion;
                isThereOrderRecord.ShipPostalCode = request.ShipPostalCode;
                isThereOrderRecord.ShipCountry = request.ShipCountry;


                _orderRepository.Update(isThereOrderRecord);
                await _orderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

