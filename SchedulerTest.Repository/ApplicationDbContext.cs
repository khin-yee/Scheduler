﻿using Microsoft.EntityFrameworkCore;
using SchedulerTest.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }
        public DbSet<Product> Product { get; set; }
        public DbSet<AdaptorProduct> AdaptorProduct { get; set; }
        public DbSet<ProductSchedule> ProductSchedule { get; set; }
    }
}
