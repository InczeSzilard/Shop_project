using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Lab5_Window
{
    public class Category
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(50)] public string Name { get; set; }
    }

    public class Product
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(30)] public string Name { get; set; }
        [MaxLength(500)] public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        // Only for showing in the grid, not saved in the database
        [NotMapped] public string CategoryName { get; set; }
    }

    [Table("SaleHistories")]
    public class SaleHistory
    {
        [Key] public int Id { get; set; }
        public int ProductId { get; set; }
        [MaxLength(30)] public string ProductName { get; set; }
        [MaxLength(50)] public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public DateTime SoldAt { get; set; }
    }

    public class StoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SaleHistory> SaleHistories { get; set; }

        private static readonly object tableLock = new object();
        private static bool tablesCreated = false;

        public StoreDbContext() : base("name=StoreDb")
        {
            // SQLite: we create the tables ourselves, EF must not try to
            Database.SetInitializer<StoreDbContext>(null);
        }

        // Creates the tables if they don't exist. Runs only once per program start.
        public static void CreateTables()
        {
            lock (tableLock)
            {
                if (tablesCreated) return;

                using (StoreDbContext db = new StoreDbContext())
                {
                    db.Database.ExecuteSqlCommand(
                        @"CREATE TABLE IF NOT EXISTS Categories (
                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                              Name TEXT NOT NULL)");

                    db.Database.ExecuteSqlCommand(
                        @"CREATE TABLE IF NOT EXISTS Products (
                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                              Name TEXT NOT NULL,
                              Description TEXT,
                              EntryDate DATETIME NOT NULL,
                              ExpiryDate DATETIME NOT NULL,
                              Quantity INTEGER NOT NULL,
                              CategoryId INTEGER NOT NULL)");

                    db.Database.ExecuteSqlCommand(
                        @"CREATE TABLE IF NOT EXISTS SaleHistories (
                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                              ProductId INTEGER NOT NULL,
                              ProductName TEXT,
                              CategoryName TEXT,
                              Quantity INTEGER NOT NULL,
                              SoldAt DATETIME NOT NULL)");
                }

                tablesCreated = true;
            }
        }
    }
}