
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

namespace Business.Handlers.CustomerCustomerDemoes.Queries
{

    public class GetCustomerCustomerDemoesQuery : IRequest<IDataResult<IEnumerable<CustomerCustomerDemo>>>
    {
        public class GetCustomerCustomerDemoesQueryHandler : IRequestHandler<GetCustomerCustomerDemoesQuery, IDataResult<IEnumerable<CustomerCustomerDemo>>>
        {
            private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;
            private readonly IMediator _mediator;

            public GetCustomerCustomerDemoesQueryHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository, IMediator mediator)
            {
                _customerCustomerDemoRepository = customerCustomerDemoRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<CustomerCustomerDemo>>> Handle(GetCustomerCustomerDemoesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<CustomerCustomerDemo>>(await _customerCustomerDemoRepository.GetListAsync());
            }
        }
    }
}