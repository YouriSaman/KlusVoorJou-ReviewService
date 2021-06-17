using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReviewService.Models;

namespace ReviewService.DAL
{
    public class ReviewDbContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options)
        {
            
        }
    }
}
