
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.CustomerCustomerDemoes.Queries
{
    public class GetCustomerCustomerDemoQuery : IRequest<IDataResult<CustomerCustomerDemo>>
    {
        public string CustomerId { get; set; }

        public class GetCustomerCustomerDemoQueryHandler : IRequestHandler<GetCustomerCustomerDemoQuery, IDataResult<CustomerCustomerDemo>>
        {
            private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;
            private readonly IMediator _mediator;

            public GetCustomerCustomerDemoQueryHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository, IMediator mediator)
            {
                _customerCustomerDemoRepository = customerCustomerDemoRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<CustomerCustomerDemo>> Handle(GetCustomerCustomerDemoQuery request, CancellationToken cancellationToken)
            {
                var customerCustomerDemo = await _customerCustomerDemoRepository.GetAsync(p => p.CustomerId == request.CustomerId);
                return new SuccessDataResult<CustomerCustomerDemo>(customerCustomerDemo);
            }
        }
    }
}
