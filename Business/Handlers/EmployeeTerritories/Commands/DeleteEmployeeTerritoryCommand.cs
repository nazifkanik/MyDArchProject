
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


namespace Business.Handlers.EmployeeTerritories.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteEmployeeTerritoryCommand : IRequest<IResult>
    {
        public int EmployeeId { get; set; }

        public class DeleteEmployeeTerritoryCommandHandler : IRequestHandler<DeleteEmployeeTerritoryCommand, IResult>
        {
            private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
            private readonly IMediator _mediator;

            public DeleteEmployeeTerritoryCommandHandler(IEmployeeTerritoryRepository employeeTerritoryRepository, IMediator mediator)
            {
                _employeeTerritoryRepository = employeeTerritoryRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEmployeeTerritoryCommand request, CancellationToken cancellationToken)
            {
                var employeeTerritoryToDelete = _employeeTerritoryRepository.Get(p => p.EmployeeId == request.EmployeeId);

                _employeeTerritoryRepository.Delete(employeeTerritoryToDelete);
                await _employeeTerritoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

