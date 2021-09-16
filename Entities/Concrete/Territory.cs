using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Territory : IEntity
    {
        public string TerritoryId { get; set; }
        public int RegionId { get; set; }
        public string TerritoryDescription { get; set; }

    }
    
    public class EmployeeTerritory : IEntity
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }

    }
    
    public class Employee : IEntity
    {
        public int EmployeeId { get; set; }
        public int ReportsTo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime BirtHireDatehDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public string Notes { get; set; }
        public string PhotoPath { get; set; }
    }
}
