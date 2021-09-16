
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class CustomerDemographicRepository : EfEntityRepositoryBase<CustomerDemographic, ProjectDbContext>, ICustomerDemographicRepository
    {
        public CustomerDemographicRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
