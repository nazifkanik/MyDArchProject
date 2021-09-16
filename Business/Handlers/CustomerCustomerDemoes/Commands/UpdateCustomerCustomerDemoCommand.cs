
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
using Business.Handlers.CustomerCustomerDemoes.ValidationRules;


namespace Business.Handlers.CustomerCustomerDemoes.Commands
{


    public class UpdateCustomerCustomerDemoCommand : IRequest<IResult>
    {
        public string CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public class UpdateCustomerCustomerDemoCommandHandler : IRequestHandler<UpdateCustomerCustomerDemoCommand, IResult>
        {
            private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;
            private readonly IMediator _mediator;

            public UpdateCustomerCustomerDemoCommandHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository, IMediator mediator)
            {
                _customerCustomerDemoRepository = customerCustomerDemoRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCustomerCustomerDemoValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCustomerCustomerDemoCommand request, CancellationToken cancellationToken)
            {
                var isThereCustomerCustomerDemoRecord = await _customerCustomerDemoRepository.GetAsync(u => u.CustomerId == request.CustomerId);


                isThereCustomerCustomerDemoRecord.CustomerTypeId = request.CustomerTypeId;


                _customerCustomerDemoRepository.Update(isThereCustomerCustomerDemoRecord);
                await _customerCustomerDemoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

