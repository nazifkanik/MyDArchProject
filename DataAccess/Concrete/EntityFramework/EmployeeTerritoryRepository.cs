
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class EmployeeTerritoryRepository : EfEntityRepositoryBase<EmployeeTerritory, ProjectDbContext>, IEmployeeTerritoryRepository
    {
        public EmployeeTerritoryRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
