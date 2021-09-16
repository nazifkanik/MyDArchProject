
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SupplierRepository : EfEntityRepositoryBase<Supplier, ProjectDbContext>, ISupplierRepository
    {
        public SupplierRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
