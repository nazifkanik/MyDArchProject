
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


namespace Business.Handlers.Suppliers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSupplierCommand : IRequest<IResult>
    {
        public int SupplierId { get; set; }

        public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, IResult>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMediator _mediator;

            public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository, IMediator mediator)
            {
                _supplierRepository = supplierRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
            {
                var supplierToDelete = _supplierRepository.Get(p => p.SupplierId == request.SupplierId);

                _supplierRepository.Delete(supplierToDelete);
                await _supplierRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

