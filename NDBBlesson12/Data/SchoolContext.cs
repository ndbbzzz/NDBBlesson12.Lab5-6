using Microsoft.EntityFrameworkCore;

using NDBBlesson12.Models;

namespace NDBBlesson12.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Learner> Learners { get; set; }
        public DbSet<Major> Majors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Learner>().ToTable("Learner");
            modelBuilder.Entity<Major>().ToTable("Major");
        }
    }
}