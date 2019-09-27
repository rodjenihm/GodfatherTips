using System;
using System.Collections.Generic;
using System.Text;
using GodfatherTips.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GodfatherTips.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>()
                .HasOne(a => a.Author)
                .WithMany(d => d.Posts)
                .HasForeignKey(d => d.AuthorId);
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tip> Tips { get; set; }
    }
}
