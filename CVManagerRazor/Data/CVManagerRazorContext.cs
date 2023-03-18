using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CVManagerRazor.Models;

namespace CVManagerRazor.Data
{
    public class CVManagerRazorContext : DbContext
    {
        public CVManagerRazorContext (DbContextOptions<CVManagerRazorContext> options)
            : base(options)
        {
        }

        public DbSet<Entry> Entry { get; set; } = default!;
    }
}
