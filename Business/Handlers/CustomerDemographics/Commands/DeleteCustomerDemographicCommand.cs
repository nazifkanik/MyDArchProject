
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


namespace Business.Handlers.CustomerDemographics.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCustomerDemographicCommand : IRequest<IResult>
    {
        public string CustomerTypeId { get; set; }

        public class DeleteCustomerDemographicCommandHandler : IRequestHandler<DeleteCustomerDemographicCommand, IResult>
        {
            private readonly ICustomerDemographicRepository _customerDemographicRepository;
            private readonly IMediator _mediator;

            public DeleteCustomerDemographicCommandHandler(ICustomerDemographicRepository customerDemographicRepository, IMediator mediator)
            {
                _customerDemographicRepository = customerDemographicRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCustomerDemographicCommand request, CancellationToken cancellationToken)
            {
                var customerDemographicToDelete = _customerDemographicRepository.Get(p => p.CustomerTypeId == request.CustomerTypeId);

                _customerDemographicRepository.Delete(customerDemographicToDelete);
                await _customerDemographicRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

