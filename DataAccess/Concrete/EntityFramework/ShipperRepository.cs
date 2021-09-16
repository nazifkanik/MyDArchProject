
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ShipperRepository : EfEntityRepositoryBase<Shipper, ProjectDbContext>, IShipperRepository
    {
        public ShipperRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
