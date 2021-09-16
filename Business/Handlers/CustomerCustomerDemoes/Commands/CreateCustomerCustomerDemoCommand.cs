
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
using Business.Handlers.CustomerCustomerDemoes.ValidationRules;

namespace Business.Handlers.CustomerCustomerDemoes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCustomerCustomerDemoCommand : IRequest<IResult>
    {

        public string CustomerTypeId { get; set; }
        public string CustomerId { get; set; }

        public class CreateCustomerCustomerDemoCommandHandler : IRequestHandler<CreateCustomerCustomerDemoCommand, IResult>
        {
            private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;
            private readonly IMediator _mediator;
            public CreateCustomerCustomerDemoCommandHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository, IMediator mediator)
            {
                _customerCustomerDemoRepository = customerCustomerDemoRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCustomerCustomerDemoValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCustomerCustomerDemoCommand request, CancellationToken cancellationToken)
            {
                var isThereCustomerCustomerDemoRecord = _customerCustomerDemoRepository.Query().Any(u => u.CustomerTypeId == request.CustomerTypeId && u.CustomerId == request.CustomerId);

                if (isThereCustomerCustomerDemoRecord == true)
                {
                    return new ErrorResult(Messages.NameAlreadyExist);
                }

                var addedCustomerCustomerDemo = new CustomerCustomerDemo
                {
                    CustomerId = request.CustomerId,
                    CustomerTypeId = request.CustomerTypeId,
                };

                _customerCustomerDemoRepository.Add(addedCustomerCustomerDemo);
                await _customerCustomerDemoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}