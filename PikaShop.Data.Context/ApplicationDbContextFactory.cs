using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace PikaShop.Data.Context
{
    /// <summary>
    /// This Factory Creates an ApplicationDbContext object only in design time
    /// which is only used for development.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("data source=.;initial catalog=PikaShop;integrated security=true;encrypt=false", b => b.MigrationsAssembly("PikaShop.Admin"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
