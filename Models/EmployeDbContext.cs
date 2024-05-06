using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_with_Ajax.Models
{
    public class EmployeDbContext:DbContext
    {
 
        public EmployeDbContext(DbContextOptions options):base(options) { }
        public DbSet<Employe>Employes { get; set; }
    }
}
