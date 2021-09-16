
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Suppliers.Queries
{
    public class GetSupplierQuery : IRequest<IDataResult<Supplier>>
    {
        public int SupplierId { get; set; }

        public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, IDataResult<Supplier>>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMediator _mediator;

            public GetSupplierQueryHandler(ISupplierRepository supplierRepository, IMediator mediator)
            {
                _supplierRepository = supplierRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Supplier>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
            {
                var supplier = await _supplierRepository.GetAsync(p => p.SupplierId == request.SupplierId);
                return new SuccessDataResult<Supplier>(supplier);
            }
        }
    }
}
