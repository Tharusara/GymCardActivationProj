using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtuagymDevTask.Data;
using VirtuagymDevTask.DTOs;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly IGymRepository _repo;
        private readonly IMapper _mapper;

        public CheckInService(IGymRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<User> CheckUserValidity(int id)
        {
            // check if the user exists
            var user = await _repo.GetUser(id);
            if (user == null) return null;

            // check user membership validity
            if (user.Memberships.Count <= 0)
            {
                user.ContactNo = 0;
                return user;
            }

            MembershipToUpdateDTO membershipCredits = new();
            foreach (var membership in user.Memberships)
            {
                if (membership.Status != (MembershipStatus)1)
                {
                    user.ContactNo = 1;
                    return user;
                }
                if (membership.Credits <= 0)
                {
                    user.ContactNo = 2;
                    return user;
                }
                // Map data after validation
                membershipCredits.Credits = membership.Credits - 1;
                membershipCredits.Id = membership.Id;
                membershipCredits.UserId = id;
                membershipCredits.Id = membership.Id;
                membershipCredits.Status = membership.Status;
                membershipCredits.MembershipType = membership.MembershipType;
                membershipCredits.Start = membership.Start;
                membershipCredits.End = membership.End;
            }

            // if valid then subtract 1 credit from the membership credits and update changes
            var membershipFromRepo = await _repo.GetMembership(membershipCredits.Id);
            _mapper.Map(membershipCredits, membershipFromRepo);

            // add invoice line to the invoice
            var invoiceId = 0;
            if (user.Invoices.Count > 0)
            {
                foreach (var invoice in user.Invoices)
                {
                    invoiceId = invoice.Id;
                }
                var invoiceLine = new InvoiceLines
                {
                    Description = $"Member {user.FullName} checkedIn {DateTime.Now}",
                    Amount = 20,
                    InvoiceId = invoiceId
                };
                _repo.Add(invoiceLine);
            }
            else
            {
                //  add invoice with a invoice line if it doesnt exists
                ICollection<InvoiceLines> invoiceLines = new List<InvoiceLines>();
                var invoiceLine = new InvoiceLines
                {
                    Description = $"Member {user.FullName} checkedIn {DateTime.Now}",
                    Amount = 20,
                };
                invoiceLines.Add(invoiceLine);
                var invoice = new Invoice
                {
                    DueDate = DateTime.Now,
                    Description = $"Member {user.FullName} checkedIn {DateTime.Now}",
                    Amount = 20,
                    Month = DateTime.Now.Month.ToString(),
                    Status = (InvoiceStatus)1,
                    UserId = id,
                    Lines = invoiceLines
                };
                _repo.Add(invoice);
            }

            // save changes to db
            if (await _repo.SaveAll())
                return user;
            user.ContactNo = 3;
            return user;
        }

    }
}
