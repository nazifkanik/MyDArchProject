
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.EmployeeTerritories.ValidationRules;


namespace Business.Handlers.EmployeeTerritories.Commands
{


    public class UpdateEmployeeTerritoryCommand : IRequest<IResult>
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }

        public class UpdateEmployeeTerritoryCommandHandler : IRequestHandler<UpdateEmployeeTerritoryCommand, IResult>
        {
            private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
            private readonly IMediator _mediator;

            public UpdateEmployeeTerritoryCommandHandler(IEmployeeTerritoryRepository employeeTerritoryRepository, IMediator mediator)
            {
                _employeeTerritoryRepository = employeeTerritoryRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEmployeeTerritoryValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEmployeeTerritoryCommand request, CancellationToken cancellationToken)
            {
                var isThereEmployeeTerritoryRecord = await _employeeTerritoryRepository.GetAsync(u => u.EmployeeId == request.EmployeeId);


                isThereEmployeeTerritoryRecord.TerritoryId = request.TerritoryId;


                _employeeTerritoryRepository.Update(isThereEmployeeTerritoryRecord);
                await _employeeTerritoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

