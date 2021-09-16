
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
using Business.Handlers.Suppliers.ValidationRules;


namespace Business.Handlers.Suppliers.Commands
{


    public class UpdateSupplierCommand : IRequest<IResult>
    {
        public int SupplierId { get; set; }
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

        public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, IResult>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMediator _mediator;

            public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository, IMediator mediator)
            {
                _supplierRepository = supplierRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSupplierValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
            {
                var isThereSupplierRecord = await _supplierRepository.GetAsync(u => u.SupplierId == request.SupplierId);


                isThereSupplierRecord.CompanyName = request.CompanyName;
                isThereSupplierRecord.ContactName = request.ContactName;
                isThereSupplierRecord.ContactTitle = request.ContactTitle;
                isThereSupplierRecord.Address = request.Address;
                isThereSupplierRecord.City = request.City;
                isThereSupplierRecord.Region = request.Region;
                isThereSupplierRecord.PostalCode = request.PostalCode;
                isThereSupplierRecord.Country = request.Country;
                isThereSupplierRecord.Phone = request.Phone;
                isThereSupplierRecord.Fax = request.Fax;
                isThereSupplierRecord.HomePage = request.HomePage;


                _supplierRepository.Update(isThereSupplierRecord);
                await _supplierRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

