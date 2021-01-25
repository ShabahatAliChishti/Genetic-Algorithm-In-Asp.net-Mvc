using Microsoft.EntityFrameworkCore;
using TimeTable.Core.Models;

namespace TimeTableServer.Context
{
    public class DataContext : DbContext
    {
        public DbSet<StudentCourse> StudentCourse { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\ProjectsV13;Database=TimeTableDb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey("StudentId", "CourseId");
            modelBuilder.Entity<Student>().HasMany(student => student.Courses);
            modelBuilder.Entity<Course>().HasMany(course => course.Students);
            modelBuilder.Entity<Student>().HasMany<TimeSlot>(student => student.TimeSlots);
            modelBuilder.Entity<TimeSlot>().HasMany<Student>(slot => slot.Students);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}