
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

namespace Business.Handlers.Employees.Queries
{

    public class GetEmployeesQuery : IRequest<IDataResult<IEnumerable<Employee>>>
    {
        public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IDataResult<IEnumerable<Employee>>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;

            public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMediator mediator)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Employee>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Employee>>(await _employeeRepository.GetListAsync());
            }
        }
    }
}