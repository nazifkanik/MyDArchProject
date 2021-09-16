using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CustomerCustomerDemo : IEntity
    {
        public string CustomerId { get; set; }
        public string CustomerTypeId { get; set; }
    }
}
