using CrudMvcEntity.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrudMvcEntity.Data
{
    public class MvcDemoDBContext : DbContext
    {
        public MvcDemoDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bakery> Bakery { get; set; }
    }
}
