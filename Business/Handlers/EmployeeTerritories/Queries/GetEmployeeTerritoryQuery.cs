
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.EmployeeTerritories.Queries
{
    public class GetEmployeeTerritoryQuery : IRequest<IDataResult<EmployeeTerritory>>
    {
        public int EmployeeId { get; set; }

        public class GetEmployeeTerritoryQueryHandler : IRequestHandler<GetEmployeeTerritoryQuery, IDataResult<EmployeeTerritory>>
        {
            private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
            private readonly IMediator _mediator;

            public GetEmployeeTerritoryQueryHandler(IEmployeeTerritoryRepository employeeTerritoryRepository, IMediator mediator)
            {
                _employeeTerritoryRepository = employeeTerritoryRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<EmployeeTerritory>> Handle(GetEmployeeTerritoryQuery request, CancellationToken cancellationToken)
            {
                var employeeTerritory = await _employeeTerritoryRepository.GetAsync(p => p.EmployeeId == request.EmployeeId);
                return new SuccessDataResult<EmployeeTerritory>(employeeTerritory);
            }
        }
    }
}
