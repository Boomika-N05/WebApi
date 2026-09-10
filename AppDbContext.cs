using System;
using Microsoft.EntityFrameworkCore;

namespace WebApiProject
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}