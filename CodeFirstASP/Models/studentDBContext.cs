using Microsoft.EntityFrameworkCore;

namespace CodeFirstASP.Models
{
    public class studentDBContext : DbContext //
    {
        public studentDBContext(DbContextOptions options):base(options)//to 
        {
                
        }
        public DbSet<student> students { get; set; }//it will represent the table name
    }
}
