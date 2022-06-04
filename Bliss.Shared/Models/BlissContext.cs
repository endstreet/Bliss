using System;
using System.Collections.Generic;
using Bliss.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bliss.Models
{
    public partial class BlissContext : DbContext
    {
        public DbSet<Cruise> Cruises { get; set; }
        public DbSet<WayPoint> WayPoints { get; set; }
        public DbSet<Device> Devices { get; set; }
        public BlissContext()
        {
        }

        public BlissContext(DbContextOptions<BlissContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Filename=D:\\\\FollowTheSun\\\\Bliss.Db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
