//using ClassLibrary;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace WPF_invoiceApp.context
//{
//    public class InvoiceAppDBInitializer
//    {
//        public static bool IsPerformed { get; set; } = false;

//        public static void Initialize(DatabaseContext context)
//        {
//            var taxes = new Tax[]
//            {
//                new Tax() { Name = "VAT", Description = "Podatek VAT", TaxAmount = "23%" },
//                new Tax() { Name = "PIT", Description = "Podatek PIT", TaxAmount = "11%" },
//                new Tax() { Name = "CIT", Description = "Podatek CIT", TaxAmount = "8%" }
//            };

//            foreach(Tax t in taxes)
//            {
//                context.Taxes.Add(t);
//            }

//            context.SaveChanges();

//            var companies = new Company[]
//            {
//                new Company() { CompanyName = "AGH", Address = "Czarnowiejska 16", City = "Kraków", Country = "Polska", PostalCode = "30-054" },
//                new Company() { CompanyName = "WSEI", Address = "św. Filipa 17", City = "Kraków", Country = "Polska", PostalCode = "30-210" }
//            };

//            foreach(Company c in companies)
//            {
//                context.Companies.Add(c);
//            }

//            context.SaveChanges();

//            var addreses = new Address[] 
//            {
//                new Address() { AddressName = "Aleja Pokoju 24, Sieradz", Country = "Polska" },
//                new Address() { AddressName = "Zawojska 12, Łódź", Country = "Polska" },
//                new Address() { AddressName = "Rydygiera 36, Kraków", Country = "Polska" }
//            };

//            foreach(Address a in addreses)
//            {
//                context.Addresses.Add(a);
//            }

//            context.SaveChanges();

//            var customers = new Customer[]
//            { 
//                new Customer() 
//                { 
//                    Name = "Hubert Strumiński", 
//                    Email = "hubert@wp.pl", 
//                    Address = addreses[0],
//                    Nip = "75356754", 
//                    PhoneNumber = "+48 56 12 34 12",
//                    Website = "www.divelog.eu", 
//                    Note = "To jest komentarz. To jest komentarz. To jest komentarz.To jest komentarz. To jest komentarz." 
//                },
//                new Customer()
//                {
//                    Name = "Jacek Ryś",
//                    Email = "jacek@onet.pl",
//                    Address = addreses[1],
//                    Nip = "7653278",
//                    PhoneNumber = "+48 12 45 98 21",
//                    Website = "www.scuba-book.com",
//                    Note = "To jest komentarz. To jest komentarz. To jest komentarz.To jest komentarz. To jest komentarz."
//                },
//                new Customer()
//                {
//                    Name = "Andrzej Biały",
//                    Email = "andrzej@gmail.com",
//                    Address = addreses[2],
//                    Nip = "57590743",
//                    PhoneNumber = "+67 32 98 67 12",
//                    Website = "www.e.wsei.edu.pl",
//                    Note = "To jest komentarz. To jest komentarz. To jest komentarz.To jest komentarz. To jest komentarz."
//                }
//            };

//            foreach(Customer c in customers)
//            {
//                context.Customers.Add(c);
//            }

//            context.SaveChanges();

//            var products = new Product[] 
//            {
//                new Product() 
//                { 
//                    Name = "Badania naukowe - grafen", 
//                    Description = "ze współpracą z jednostką PK", 
//                    Amount = 1, 
//                    Discount = 15, 
//                    Price = 47600,
//                    Unit = "TIME", 
//                    Tax = taxes[0] 
//                },
//                new Product()
//                {
//                    Name = "Odśnieżanie dachu",
//                    Description = "",
//                    Amount = 1,
//                    Discount = 16,
//                    Price = 7200,
//                    Unit = "TIME",
//                    Tax = taxes[1]
//                },
//                new Product()
//                {
//                    Name = "Budowa nowego kompleksu edukacyjnego",
//                    Description = "To jest opis. To jest opis. To jest opis. To jest opis. To jest opis. To jest opis. To jest opis.",
//                    Amount = 24,
//                    Discount = 3,
//                    Price = 2412700,
//                    Unit = "TIME",
//                    Tax = taxes[2]
//                }
//            };

//            foreach(Product p in products)
//            {
//                context.Products.Add(p);
//            }

//            context.SaveChanges();

//            var invoices = new Invoice[]
//            {
//                new Invoice()
//                {
//                    Number = "848953",
//                    Date = DateTime.Now,
//                    Deadline = DateTime.Now,
//                    Status = Status.SENT,
//                    Customer = customers[0],
//                    Comment = "To jest faktura. To jest faktura. To jest faktura. To jest faktura.",
//                    Products = new List<Product>() { products[0], products[2] }
//                },
//                new Invoice()
//                {
//                    Number = "532567",
//                    Date = DateTime.Now,
//                    Deadline = DateTime.Now,
//                    Status = Status.NOT_SENT,
//                    Customer = customers[1],
//                    Comment = "To jest faktura...",
//                    Products = new List<Product>() { products[1], products[0] }
//                },
//                new Invoice()
//                {
//                    Number = "554656",
//                    Date = DateTime.Now,
//                    Deadline = DateTime.Now,
//                    Status = Status.SENT,
//                    Customer = customers[2],
//                    Comment = "",
//                    Products = new List<Product>() { products[2] }
//                },
//                new Invoice()
//                {
//                    Number = "6426789",
//                    Date = DateTime.Now,
//                    Deadline = DateTime.Now,
//                    Status = Status.NOT_SENT,
//                    Customer = customers[0],
//                    Comment = "",
//                    Products = new List<Product>() { products[1], products[2], products[0] }
//                }
//            };

//            foreach(Invoice i in invoices)
//            {
//                context.Invoices.Add(i);
//            }

//            context.SaveChanges();

//            IsPerformed = true;
//        }
//    }
//}
