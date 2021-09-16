
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.CustomerDemographics.Queries
{
    public class GetCustomerDemographicQuery : IRequest<IDataResult<CustomerDemographic>>
    {
        public string CustomerTypeId { get; set; }

        public class GetCustomerDemographicQueryHandler : IRequestHandler<GetCustomerDemographicQuery, IDataResult<CustomerDemographic>>
        {
            private readonly ICustomerDemographicRepository _customerDemographicRepository;
            private readonly IMediator _mediator;

            public GetCustomerDemographicQueryHandler(ICustomerDemographicRepository customerDemographicRepository, IMediator mediator)
            {
                _customerDemographicRepository = customerDemographicRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<CustomerDemographic>> Handle(GetCustomerDemographicQuery request, CancellationToken cancellationToken)
            {
                var customerDemographic = await _customerDemographicRepository.GetAsync(p => p.CustomerTypeId == request.CustomerTypeId);
                return new SuccessDataResult<CustomerDemographic>(customerDemographic);
            }
        }
    }
}
