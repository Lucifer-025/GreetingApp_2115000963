using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Context
{
    
        public class GreetContext : DbContext
        {
            public GreetContext(DbContextOptions<GreetContext> options) : base(options) { }
            public virtual DbSet<Entity.GreetEntity> GreetMessages { get; set; }
        }
    }

