
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrderDetailRepository : EfEntityRepositoryBase<OrderDetail, ProjectDbContext>, IOrderDetailRepository
    {
        public OrderDetailRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
