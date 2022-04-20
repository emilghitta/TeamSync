using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Users;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

//Table name. Set-ul e luat din DbSet
        public DbSet<TeamSyncUser> Users { get; set; }

    }
}