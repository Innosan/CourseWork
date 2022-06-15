using System.Data.Entity;
using CourseWork.Models;

namespace CourseWork
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
