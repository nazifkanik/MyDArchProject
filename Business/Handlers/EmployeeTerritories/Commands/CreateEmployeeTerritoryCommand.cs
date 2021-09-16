
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.EmployeeTerritories.ValidationRules;

namespace Business.Handlers.EmployeeTerritories.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEmployeeTerritoryCommand : IRequest<IResult>
    {

        public string TerritoryId { get; set; }


        public class CreateEmployeeTerritoryCommandHandler : IRequestHandler<CreateEmployeeTerritoryCommand, IResult>
        {
            private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
            private readonly IMediator _mediator;
            public CreateEmployeeTerritoryCommandHandler(IEmployeeTerritoryRepository employeeTerritoryRepository, IMediator mediator)
            {
                _employeeTerritoryRepository = employeeTerritoryRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEmployeeTerritoryValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEmployeeTerritoryCommand request, CancellationToken cancellationToken)
            {
                var isThereEmployeeTerritoryRecord = _employeeTerritoryRepository.Query().Any(u => u.TerritoryId == request.TerritoryId);

                if (isThereEmployeeTerritoryRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedEmployeeTerritory = new EmployeeTerritory
                {
                    TerritoryId = request.TerritoryId,

                };

                _employeeTerritoryRepository.Add(addedEmployeeTerritory);
                await _employeeTerritoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}