using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Interfaces;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Framework.Infrastructure;
using MonoRepo.Microservice.Application.Domain.Entities;
using MonoRepo.Microservice.Application.Domain.Views;
using MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations;

namespace MonoRepo.Microservice.Application.Infrastructure
{
    public class ApplicationDbContext : BaseDbContext
    {
        // Tables
        public DbSet<Domain.Aggregates.DegreeAggregate.Degree> Degree { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<DegreeType> DegreeType { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<InstructorCourse> InstructorCourse { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }

        // Views
        public DbSet<DegreeSummary> DegreeSummary { get; set; }

        // Ctors
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options, IMediator mediator, IdentityUser user, IDateTime dateTime) : base(options, mediator, user, dateTime)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(DegreeConfiguration).Assembly);
        }
    }
}
