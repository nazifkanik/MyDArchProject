
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

namespace Business.Handlers.EmployeeTerritories.Queries
{

    public class GetEmployeeTerritoriesQuery : IRequest<IDataResult<IEnumerable<EmployeeTerritory>>>
    {
        public class GetEmployeeTerritoriesQueryHandler : IRequestHandler<GetEmployeeTerritoriesQuery, IDataResult<IEnumerable<EmployeeTerritory>>>
        {
            private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
            private readonly IMediator _mediator;

            public GetEmployeeTerritoriesQueryHandler(IEmployeeTerritoryRepository employeeTerritoryRepository, IMediator mediator)
            {
                _employeeTerritoryRepository = employeeTerritoryRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<EmployeeTerritory>>> Handle(GetEmployeeTerritoriesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<EmployeeTerritory>>(await _employeeTerritoryRepository.GetListAsync());
            }
        }
    }
}