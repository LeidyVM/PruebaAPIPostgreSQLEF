using Microsoft.EntityFrameworkCore;
using PruebaAPIPostgreSQLEF.Modelos;

namespace PruebaAPIPostgreSQLEF.Data
{
    public class InfoDirectorio: DbContext
    {
        public InfoDirectorio(DbContextOptions<InfoDirectorio> options) : base(options) {
    }
        public DbSet<Phonebook> Phonebook => Set<Phonebook>();
    }
}
