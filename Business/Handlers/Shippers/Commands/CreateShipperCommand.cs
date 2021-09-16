
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
using Business.Handlers.Shippers.ValidationRules;

namespace Business.Handlers.Shippers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateShipperCommand : IRequest<IResult>
    {

        public string CompanyName { get; set; }
        public string Phone { get; set; }


        public class CreateShipperCommandHandler : IRequestHandler<CreateShipperCommand, IResult>
        {
            private readonly IShipperRepository _shipperRepository;
            private readonly IMediator _mediator;
            public CreateShipperCommandHandler(IShipperRepository shipperRepository, IMediator mediator)
            {
                _shipperRepository = shipperRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateShipperValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateShipperCommand request, CancellationToken cancellationToken)
            {
                var isThereShipperRecord = _shipperRepository.Query().Any(u => u.CompanyName == request.CompanyName);

                if (isThereShipperRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedShipper = new Shipper
                {
                    CompanyName = request.CompanyName,
                    Phone = request.Phone,

                };

                _shipperRepository.Add(addedShipper);
                await _shipperRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}