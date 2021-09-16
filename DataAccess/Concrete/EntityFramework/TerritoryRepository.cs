
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class TerritoryRepository : EfEntityRepositoryBase<Territory, ProjectDbContext>, ITerritoryRepository
    {
        public TerritoryRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
