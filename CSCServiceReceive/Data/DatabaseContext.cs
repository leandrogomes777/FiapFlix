using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSCServiceReceive.Models;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<CSCServiceReceive.Models.ServiceRequest> ServiceRequest { get; set; }
    }
