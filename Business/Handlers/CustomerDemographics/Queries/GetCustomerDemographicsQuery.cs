
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.CustomerDemographics.Queries
{

    public class GetCustomerDemographicsQuery : IRequest<IDataResult<IEnumerable<CustomerDemographic>>>
    {
        public class GetCustomerDemographicsQueryHandler : IRequestHandler<GetCustomerDemographicsQuery, IDataResult<IEnumerable<CustomerDemographic>>>
        {
            private readonly ICustomerDemographicRepository _customerDemographicRepository;
            private readonly IMediator _mediator;

            public GetCustomerDemographicsQueryHandler(ICustomerDemographicRepository customerDemographicRepository, IMediator mediator)
            {
                _customerDemographicRepository = customerDemographicRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<CustomerDemographic>>> Handle(GetCustomerDemographicsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<CustomerDemographic>>(await _customerDemographicRepository.GetListAsync());
            }
        }
    }
}