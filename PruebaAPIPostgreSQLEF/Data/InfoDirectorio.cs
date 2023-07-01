using Microsoft.EntityFrameworkCore;
using PruebaAPIPostgreSQLEF.Modelos;

namespace PruebaAPIPostgreSQLEF.Data
{
    public class DirectoryInformation: DbContext
    {
        public DirectoryInformation(DbContextOptions<DirectoryInformation> options) : base(options) {
    }
        public DbSet<Phonebook> Phonebook => Set<Phonebook>();
    }
}
