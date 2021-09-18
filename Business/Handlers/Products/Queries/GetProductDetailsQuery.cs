using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Products.Queries
{
    public class GetProductDetailsQuery : IRequest<IDataResult<IEnumerable<ProductDetailDto>>>
    {
        class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, IDataResult<IEnumerable<ProductDetailDto>>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProductDetailsQueryHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProductDetailDto>>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProductDetailDto>>(await _productRepository.GetProductDetails());
            }
        }
    }
}