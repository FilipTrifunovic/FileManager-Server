using FileManager.Models;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace FileManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}
        public DbSet<Words> Words {get;set;}
    }
}