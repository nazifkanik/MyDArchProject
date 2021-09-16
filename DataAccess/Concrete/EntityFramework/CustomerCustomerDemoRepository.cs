
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class CustomerCustomerDemoRepository : EfEntityRepositoryBase<CustomerCustomerDemo, ProjectDbContext>, ICustomerCustomerDemoRepository
    {
        public CustomerCustomerDemoRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
