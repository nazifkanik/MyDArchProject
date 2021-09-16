using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    
    public class CustomerDemographic : IEntity
    {
        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }
    }
}
