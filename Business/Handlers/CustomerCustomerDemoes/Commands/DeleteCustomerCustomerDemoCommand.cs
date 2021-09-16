
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


namespace Business.Handlers.CustomerCustomerDemoes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCustomerCustomerDemoCommand : IRequest<IResult>
    {
        public string CustomerId { get; set; }

        public class DeleteCustomerCustomerDemoCommandHandler : IRequestHandler<DeleteCustomerCustomerDemoCommand, IResult>
        {
            private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;
            private readonly IMediator _mediator;

            public DeleteCustomerCustomerDemoCommandHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository, IMediator mediator)
            {
                _customerCustomerDemoRepository = customerCustomerDemoRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCustomerCustomerDemoCommand request, CancellationToken cancellationToken)
            {
                var customerCustomerDemoToDelete = _customerCustomerDemoRepository.Get(p => p.CustomerId == request.CustomerId);

                _customerCustomerDemoRepository.Delete(customerCustomerDemoToDelete);
                await _customerCustomerDemoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

