using System.Collections.Generic;
using System.Threading.Tasks;
using VirtuagymDevTask.DTOs;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.Data
{   // Interface of data acess layer(GymRepository)
    public interface IGymRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();

        Task<IList<Invoice>> GetAllInvoices();
        Task<Invoice> GetInvoice(int Id);
        Invoice MapInvoice(InvoiceToCreateDTO invoiceDto);

        Task<IList<User>> GetAllUsers();
        Task<User> GetUser(int Id);
        Task<Membership> GetMembership(int Id);

    }
}
