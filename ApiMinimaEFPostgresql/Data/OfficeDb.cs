using ApiMinimaEFPostgresql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMinimaEFPostgresql.Data
{
    public class OfficeDb : DbContext
    {
        public OfficeDb(DbContextOptions<OfficeDb> options) : base(options)
        {

        }
        public DbSet<Employee> Employees => Set<Employee>();

    }
}
