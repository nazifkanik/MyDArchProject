
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Collections.Generic;
using Entities.Dtos;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductRepository : EfEntityRepositoryBase<Product, ProjectDbContext>, IProductRepository
    {
        public ProductRepository(ProjectDbContext context)
            : base(context)
        {
            _context = context;
        }

        ProjectDbContext _context;

        /// <summary>
        /// Gets Product Details.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Returns Product Details</returns>
        public async Task<IEnumerable<ProductDetailDto>> GetProductDetails()
        {
            using (_context)
            {
                var result = from p in _context.Products
                             join c in _context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitPrice = p.UnitPrice,
                                 UnitsInStock = p.UnitsInStock,
                             };
                return result.ToList();
            }
        }
    }
}
