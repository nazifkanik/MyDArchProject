
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Suppliers.Queries
{

    public class GetSuppliersQuery : IRequest<IDataResult<IEnumerable<Supplier>>>
    {
        public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, IDataResult<IEnumerable<Supplier>>>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMediator _mediator;

            public GetSuppliersQueryHandler(ISupplierRepository supplierRepository, IMediator mediator)
            {
                _supplierRepository = supplierRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Supplier>>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Supplier>>(await _supplierRepository.GetListAsync());
            }
        }
    }
}