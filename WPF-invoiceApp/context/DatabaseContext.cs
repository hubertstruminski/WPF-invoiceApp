using ClassLibrary;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WPF_invoiceApp.context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(b => b.Customer)
                .WithOne(i => i.Address)
                .HasForeignKey<Customer>(b => b.Id);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.Customer)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InvoiceProduct>()
                .HasKey(ip => new { ip.ProductId, ip.InvoiceId });

            modelBuilder.Entity<InvoiceProduct>()
                .HasOne(ip => ip.Product)
                .WithMany(i => i.InvoiceProducts)
                .HasForeignKey(ip => ip.ProductId);

            modelBuilder.Entity<InvoiceProduct>()
                 .HasOne(ip => ip.Invoice)
                 .WithMany(i => i.InvoiceProducts)
                 .HasForeignKey(ip => ip.InvoiceId);

            modelBuilder.Entity<Tax>().HasData(GetTaxes());
            modelBuilder.Entity<Company>().HasData(GetCompanies());
            modelBuilder.Entity<Address>().HasData(GetAddresses());
            modelBuilder.Entity<Customer>().HasData(GetCustomers());
            modelBuilder.Entity<Product>().HasData(GetProducts());
            modelBuilder.Entity<Invoice>().HasData(GetInvoices());
            modelBuilder.Entity<InvoiceProduct>().HasData(GetInvoiceProducts());

            base.OnModelCreating(modelBuilder);
        }

        private List<Tax> GetTaxes()
        {
            return new List<Tax>
            {
                new Tax() { Id = 1, Name = "VAT", Description = "Podatek VAT", TaxAmount = "23%" },
                new Tax() { Id = 2, Name = "PIT", Description = "Podatek PIT", TaxAmount = "11%" },
                new Tax() { Id = 3, Name = "CIT", Description = "Podatek CIT", TaxAmount = "8%" }
            };
        }

        private List<Company> GetCompanies(){
            return new List<Company>
            {
                new Company() { Id = 1, CompanyName = "AGH", Address = "Czarnowiejska 16", City = "Kraków", Country = "Polska", PostalCode = "30-054" },
                new Company() { Id = 2, CompanyName = "WSEI", Address = "św. Filipa 17", City = "Kraków", Country = "Polska", PostalCode = "30-210" }
            };
        }

        private List<Address> GetAddresses()
        {
            return new List<Address>
            {
                new Address() { Id = 1, AddressName = "Aleja Pokoju 24, Sieradz", Country = "Polska" },
                new Address() { Id = 2, AddressName = "Zawojska 12, Łódź", Country = "Polska" },
                new Address() { Id = 3, AddressName = "Rydygiera 36, Kraków", Country = "Polska" }
            };
        }

        private List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    Name = "Hubert Strumiński",
                    Email = "hubert@wp.pl",
                    AddressId = 1,
                    Nip = "75356754",
                    PhoneNumber = "+48 56 12 34 12",
                    Website = "www.divelog.eu",
                    Note = "To jest komentarz. To jest komentarz. To jest komentarz.To jest komentarz. To jest komentarz."
                },
                new Customer()
                {
                    Id = 2,
                    Name = "Jacek Ryś",
                    Email = "jacek@onet.pl",
                    AddressId = 2,
                    Nip = "7653278",
                    PhoneNumber = "+48 12 45 98 21",
                    Website = "www.scuba-book.com",
                    Note = "To jest komentarz. To jest komentarz. To jest komentarz.To jest komentarz. To jest komentarz."
                },
                new Customer()
                {
                    Id = 3,
                    Name = "Andrzej Biały",
                    Email = "andrzej@gmail.com",
                    AddressId = 3,
                    Nip = "57590743",
                    PhoneNumber = "+67 32 98 67 12",
                    Website = "www.e.wsei.edu.pl",
                    Note = "To jest komentarz. To jest komentarz. To jest komentarz.To jest komentarz. To jest komentarz."
                }
            };
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product()
                {
                    Id = 1,
                    Name = "Badania naukowe - grafen",
                    Description = "ze współpracą z jednostką PK",
                    Amount = 1,
                    Discount = 15,
                    Price = 47600,
                    Unit = "TIME",
                    TaxId = 2
                },
                new Product()
                {
                    Id = 2,
                    Name = "Odśnieżanie dachu",
                    Description = "",
                    Amount = 1,
                    Discount = 16,
                    Price = 7200,
                    Unit = "TIME",
                    TaxId = 3
                },
                new Product()
                {
                    Id = 3,
                    Name = "Budowa nowego kompleksu edukacyjnego",
                    Description = "To jest opis. To jest opis. To jest opis. To jest opis. To jest opis. To jest opis. To jest opis.",
                    Amount = 24,
                    Discount = 3,
                    Price = 2412700,
                    Unit = "TIME",
                    TaxId = 1
                }
            };
        }

        private List<Invoice> GetInvoices()
        {
            return new List<Invoice>
            {
                new Invoice()
                {
                    Id = 1,
                    Number = "848953",
                    Date = DateTime.Now,
                    Deadline = DateTime.Now,
                    Status = Status.SENT,
                    CustomerId = 2,
                    Comment = "To jest faktura. To jest faktura. To jest faktura. To jest faktura.",
                },
                new Invoice()
                {
                    Id = 2,
                    Number = "532567",
                    Date = DateTime.Now,
                    Deadline = DateTime.Now,
                    Status = Status.NOT_SENT,
                    CustomerId = 1,
                    Comment = "To jest faktura...",
                },
                new Invoice()
                {
                    Id = 3,
                    Number = "554656",
                    Date = DateTime.Now,
                    Deadline = DateTime.Now,
                    Status = Status.SENT,
                    CustomerId = 3,
                    Comment = "",
                },
                new Invoice()
                {
                    Id = 4,
                    Number = "6426789",
                    Date = DateTime.Now,
                    Deadline = DateTime.Now,
                    Status = Status.NOT_SENT,
                    CustomerId = 1,
                    Comment = "",
                }
            };
        }

        private List<InvoiceProduct> GetInvoiceProducts()
        {
            return new List<InvoiceProduct>
            {
                new InvoiceProduct() { InvoiceId = 1, ProductId = 1 },
                new InvoiceProduct() { InvoiceId = 1, ProductId = 2 },
                new InvoiceProduct() { InvoiceId = 1, ProductId = 3 },
                new InvoiceProduct() { InvoiceId = 2, ProductId = 1 },
                new InvoiceProduct() { InvoiceId = 2, ProductId = 2 },
                new InvoiceProduct() { InvoiceId = 3, ProductId = 1 },
                new InvoiceProduct() { InvoiceId = 3, ProductId = 3 },
                new InvoiceProduct() { InvoiceId = 4, ProductId = 2 },
            };
        }
    }
}
