using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    /// <summary>
    /// Product Repository.
    /// </summary>
    public interface IProductRepository : IEntityRepository<Product>
    {
        /// <summary>
        /// Gets Product Details.
        /// </summary>
        /// <returns>Returns List of Products with CategoryName.</returns>
        Task<IEnumerable<ProductDetailDto>> GetProductDetails();
    }
}
