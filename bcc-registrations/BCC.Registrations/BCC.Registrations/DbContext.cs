using BCC.EntityApi;
using BCC.Registrations.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace BCC.Registrations
{
    public class DbContext : EntityApi.BaseContext
    {
        // Add you DbSet<T> here:
        // eg. public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        public DbContext(DbContextOptions options) : base(options)
        {
        }
    }
}