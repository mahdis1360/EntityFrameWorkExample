using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School_EntityFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            schoolcontext context = new schoolcontext();
            context.Database.EnsureCreated();
        }

        public class schoolcontext:DbContext
        {
            public DbSet<Student> students { get; set; }
            public DbSet<Subject> subjects { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                             
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-41TMA4UR;Initial Catalog=DB_School;Integrated Security=True");
                base.OnConfiguring(optionsBuilder);
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<SubjectStudent>(entity =>
                 {
                    entity.HasKey(k=>
                    new {k.StudentId,k.SubjectId });

                     entity.HasOne(d => d.subjects)
                     .WithMany(e => e.subjectStudents)
                     .HasForeignKey(f => f.SubjectId);

                     entity.HasOne(d => d.students)
                     .WithMany(s => s.subjectStudents)
                     .HasForeignKey(k => k.StudentId);
                     

                 });
                base.OnModelCreating(modelBuilder);
            }

        }


        public class Student
         {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime Age { get; set; }
            public ICollection<SubjectStudent> subjectStudents { get; set; }
        }

       public class SubjectStudent
       {
            public Guid StudentId { get; set; }
            public Guid SubjectId { get; set; }
            public Student students { get; set; }
            public Subject subjects { get; set; }

        }

       public class Subject
        {
            [Key]
            public Guid subgect_id { get; set; }
            public string Name { get; set; }
            public int Term { get; set; }
            public int hours { get; set; }
            public ICollection<SubjectStudent> subjectStudents { get; set; }
        }
    }

   
}
