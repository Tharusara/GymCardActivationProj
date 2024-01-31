using System;
using System.Collections.Generic;
using System.Linq;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.Data
{
    public class SeedData
    {
        public static void SeedUsers(AppDbContext dbContext)
        {   // check if the database is empty or not
            if (!dbContext.Users.Any())
            {
                // create some custom users and test data to add
                var users = new List<User>
                {
                    new User{
                        FullName = "Vins",
                        DateOfBirth = DateTime.Today,
                        CallingName = "Vins",
                        ContactNo = 887894542,
                        Created = DateTime.Today,
                    },
                    new User{
                        FullName = "Tharu",
                        DateOfBirth = DateTime.Today,
                        CallingName = "Tharu",
                        ContactNo = 774564821,
                        Created = DateTime.Today,
                    },
                    new User{
                        FullName = "Bob",
                        DateOfBirth = DateTime.Today,
                        CallingName = "Bob",
                        ContactNo = 456789132,
                        Created = DateTime.Today,
                    }
                };

                ICollection<InvoiceLines> invoiceLines = new List<InvoiceLines>();
                var invoiceLine = new InvoiceLines
                {
                    Description = $"Initial checkedIn {DateTime.Now} record",
                    Amount = 20,
                };
                invoiceLines.Add(invoiceLine);
                var invoice = new Invoice
                {
                    DueDate = DateTime.Now,
                    Description = $"Initial checkedIn {DateTime.Now}",
                    Amount = 20,
                    Month = DateTime.Now.Month.ToString(),
                    Status = (InvoiceStatus)1,
                    UserId = 1,
                    Lines = invoiceLines
                };

                var membership = new Membership
                {
                    End = DateTime.Now.AddDays(60),
                    Credits = 20,
                    Start = DateTime.Now,
                    Status = (MembershipStatus)1,
                    UserId = 1,
                    MembershipType = (MembershipType)1
                };

                foreach (var user in users)
                {
                    dbContext.Users.Add(user);
                }
                dbContext.SaveChanges();
                var members = dbContext.Users.ToList();
                foreach (var member in members)
                {
                    invoice.UserId = member.Id;
                    membership.UserId = member.Id;
                    if(membership.UserId == 3 || membership.UserId == 6)
                    {
                        membership.Status = 0;
                    }
                    dbContext.Invoices.Add(invoice);
                    dbContext.Memberships.Add(membership);
                    dbContext.SaveChanges();
                    invoice.Id = 0;
                    membership.Id = 0;
                }                
            }
        }
    }
}
