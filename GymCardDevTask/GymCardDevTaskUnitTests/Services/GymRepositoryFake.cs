using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtuagymDevTask.Data;
using VirtuagymDevTask.DTOs;
using VirtuagymDevTask.Models;

namespace VirtuagyDevTaskUnitTests.Services
{
    class GymRepositoryFake : IGymRepository
    {
        private readonly IList<Invoice> _Invoices;
        private readonly IList<User> _users;

        public GymRepositoryFake()
        {
            _users = new List<User>()
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
            _Invoices = new List<Invoice>()
            {
                new Invoice
                {
                    Id =1,
                    Month="August",
                    DueDate=DateTime.Now,
                    Amount=2000,
                    Description="test1",
                    Status=0,
                    UserId=1,
                    Lines={}
                },
                new Invoice
                {
                    Id =2,
                    Month="January",
                    DueDate=DateTime.Now,
                    Amount=4000,
                    Description="test2",
                    Status=0,
                    UserId=2,
                    Lines={}
                },
                new Invoice
                {
                    Id =3,
                    Month="March",
                    DueDate=DateTime.Now,
                    Amount=6000,
                    Description="test3",
                    Status=(InvoiceStatus)1,
                    UserId=1,
                    Lines={}
                }
            };
        }

        public void Add<T>(T entity) where T : class
        {
            var entityAdded = entity;
        }

        public void Delete<T>(T entity) where T : class
        {
            var existing = _Invoices.First(a => a.Id.Equals(entity));
            var existingUser = _users.First(a => a.Id.Equals(entity));
            _Invoices.Remove(existing);
            _users.Remove(existingUser);
        }

        public async Task<IList<Invoice>> GetAllInvoices()
        {
            return _Invoices;
        }

        public async Task<IList<User>> GetAllUsers()
        {
            return _users;
        }

        public async Task<Invoice> GetInvoice(int Id)
        {
            var item = _Invoices.FirstOrDefault(a => a.Id.Equals(Id));
            return item;
        }

        public async Task<Membership> GetMembership(int Id)
        {
            var existing = _Invoices.First(a => a.Id.Equals(Id));
            var existingUser = _users.First(a => a.Id.Equals(Id));
            _Invoices.Remove(existing);
            _users.Remove(existingUser);
            return new Membership() { };
        }

        public async Task<User> GetUser(int Id)
        {
            var item = _users.FirstOrDefault(a => a.Id.Equals(Id));
            return item;
        }

        public Invoice MapInvoice(InvoiceToCreateDTO invoiceDto)
        {
            Invoice invoice = new();
            invoice.DueDate = invoiceDto.DueDate;
            invoice.Description = invoiceDto.Description;
            invoice.Amount = invoiceDto.Amount;
            invoice.Month = invoiceDto.Month;
            invoice.Status = invoiceDto.Status;
            if (invoiceDto.UserId == 0)
                return null;
            invoice.UserId = invoiceDto.UserId;
            return invoice;
        }

        public async Task<bool> SaveAll()
        {
            return true;
        }
    }
}
