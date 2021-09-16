
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
using Business.Handlers.Shippers.ValidationRules;


namespace Business.Handlers.Shippers.Commands
{


    public class UpdateShipperCommand : IRequest<IResult>
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public class UpdateShipperCommandHandler : IRequestHandler<UpdateShipperCommand, IResult>
        {
            private readonly IShipperRepository _shipperRepository;
            private readonly IMediator _mediator;

            public UpdateShipperCommandHandler(IShipperRepository shipperRepository, IMediator mediator)
            {
                _shipperRepository = shipperRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateShipperValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateShipperCommand request, CancellationToken cancellationToken)
            {
                var isThereShipperRecord = await _shipperRepository.GetAsync(u => u.ShipperId == request.ShipperId);


                isThereShipperRecord.CompanyName = request.CompanyName;
                isThereShipperRecord.Phone = request.Phone;


                _shipperRepository.Update(isThereShipperRecord);
                await _shipperRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

