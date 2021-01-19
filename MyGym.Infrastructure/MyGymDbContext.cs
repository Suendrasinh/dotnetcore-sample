using Microsoft.EntityFrameworkCore;
using MyGym.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGym.Infrastructure
{
    public class MyGymDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
