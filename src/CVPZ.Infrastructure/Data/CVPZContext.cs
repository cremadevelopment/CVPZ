using CVPZ.Domain;
using Microsoft.EntityFrameworkCore;

namespace CVPZ.Infrastructure.Data;

public class CVPZContext : DbContext
{
    public CVPZContext(DbContextOptions<CVPZContext> options) : base(options)
    { }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<User> Users { get; set; }

}
