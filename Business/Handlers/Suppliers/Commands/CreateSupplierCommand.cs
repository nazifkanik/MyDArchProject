
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
using Business.Handlers.Suppliers.ValidationRules;

namespace Business.Handlers.Suppliers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSupplierCommand : IRequest<IResult>
    {

        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }


        public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, IResult>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMediator _mediator;
            public CreateSupplierCommandHandler(ISupplierRepository supplierRepository, IMediator mediator)
            {
                _supplierRepository = supplierRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSupplierValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
            {
                var isThereSupplierRecord = _supplierRepository.Query().Any(u => u.CompanyName == request.CompanyName);

                if (isThereSupplierRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSupplier = new Supplier
                {
                    CompanyName = request.CompanyName,
                    ContactName = request.ContactName,
                    ContactTitle = request.ContactTitle,
                    Address = request.Address,
                    City = request.City,
                    Region = request.Region,
                    PostalCode = request.PostalCode,
                    Country = request.Country,
                    Phone = request.Phone,
                    Fax = request.Fax,
                    HomePage = request.HomePage,

                };

                _supplierRepository.Add(addedSupplier);
                await _supplierRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}