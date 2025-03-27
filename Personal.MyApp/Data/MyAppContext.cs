using Microsoft.EntityFrameworkCore;
using Personal.MyApp.Models;

namespace Personal.MyApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
    }
}
