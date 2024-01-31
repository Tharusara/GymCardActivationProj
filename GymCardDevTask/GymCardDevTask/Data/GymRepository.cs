using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtuagymDevTask.DTOs;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.Data
{   // Implementation of data acess layer(GymRepository)
    public class GymRepository : IGymRepository
    {
        private readonly AppDbContext _context;
        public GymRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IList<Invoice>> GetAllInvoices()
        {
            return  await _context.Invoices.Include(t => t.Lines).ToListAsync();
        }

        public async Task<Invoice> GetInvoice(int Id)
        {
            var invoices = await _context.Invoices.Include(t => t.Lines)
                .FirstOrDefaultAsync(u => u.Id == Id);
            return invoices;
        }

        public async Task<User> GetUser(int Id)
        {
            var user = await _context.Users.Include(t => t.Memberships)
                .Include(s => s.Invoices).ThenInclude(invoice => invoice.Lines)
                .FirstOrDefaultAsync(u => u.Id == Id);
            return user;
        }

        public async Task<Membership> GetMembership(int Id)
        {
            var membership = await _context.Memberships.FirstOrDefaultAsync(u => u.Id == Id);
            return membership;
        }

        public async Task<IList<User>> GetAllUsers()
        {
            return await _context.Users.Include(t => t.Memberships).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Invoice MapInvoice(InvoiceToCreateDTO invoiceDto)
        {
            
            Invoice invoice = new();
            ICollection<InvoiceLines> invoiceLines = new List<InvoiceLines>();
            var invoiceLine = new InvoiceLines
            {
                Description = invoiceDto.Lines.Description,
                Amount = invoiceDto.Lines.Amount,
            };
            invoiceLines.Add(invoiceLine);
            invoice.DueDate = invoiceDto.DueDate;
            invoice.Description = invoiceDto.Description;
            invoice.Amount = invoiceDto.Amount;
            invoice.Month = invoiceDto.Month;
            invoice.Lines = invoiceLines;
            invoice.Status = invoiceDto.Status;
            invoice.UserId = invoiceDto.UserId;
            return invoice;
        }

    }
}
