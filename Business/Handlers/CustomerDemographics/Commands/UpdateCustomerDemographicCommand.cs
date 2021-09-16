
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
using Business.Handlers.CustomerDemographics.ValidationRules;


namespace Business.Handlers.CustomerDemographics.Commands
{


    public class UpdateCustomerDemographicCommand : IRequest<IResult>
    {
        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }

        public class UpdateCustomerDemographicCommandHandler : IRequestHandler<UpdateCustomerDemographicCommand, IResult>
        {
            private readonly ICustomerDemographicRepository _customerDemographicRepository;
            private readonly IMediator _mediator;

            public UpdateCustomerDemographicCommandHandler(ICustomerDemographicRepository customerDemographicRepository, IMediator mediator)
            {
                _customerDemographicRepository = customerDemographicRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCustomerDemographicValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCustomerDemographicCommand request, CancellationToken cancellationToken)
            {
                var isThereCustomerDemographicRecord = await _customerDemographicRepository.GetAsync(u => u.CustomerTypeId == request.CustomerTypeId);


                isThereCustomerDemographicRecord.CustomerDesc = request.CustomerDesc;


                _customerDemographicRepository.Update(isThereCustomerDemographicRecord);
                await _customerDemographicRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

