
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RegionRepository : EfEntityRepositoryBase<Region, ProjectDbContext>, IRegionRepository
    {
        public RegionRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
