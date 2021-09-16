
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Employees.Queries
{
    public class GetEmployeeQuery : IRequest<IDataResult<Employee>>
    {
        public int EmployeeId { get; set; }

        public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, IDataResult<Employee>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;

            public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMediator mediator)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Employee>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetAsync(p => p.EmployeeId == request.EmployeeId);
                return new SuccessDataResult<Employee>(employee);
            }
        }
    }
}
