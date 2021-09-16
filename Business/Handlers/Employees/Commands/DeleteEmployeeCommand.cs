
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


namespace Business.Handlers.Employees.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteEmployeeCommand : IRequest<IResult>
    {
        public int EmployeeId { get; set; }

        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, IResult>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;

            public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMediator mediator)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                var employeeToDelete = _employeeRepository.Get(p => p.EmployeeId == request.EmployeeId);

                _employeeRepository.Delete(employeeToDelete);
                await _employeeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

