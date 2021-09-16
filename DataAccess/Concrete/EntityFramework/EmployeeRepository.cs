
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class EmployeeRepository : EfEntityRepositoryBase<Employee, ProjectDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
