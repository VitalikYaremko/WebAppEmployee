using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAppEmployee.Domain.Models;

namespace WebAppEmployee.Data.Context
{
    public class WebAppContext : DbContext
    {
        public WebAppContext() : base("DefaultConnection") { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}