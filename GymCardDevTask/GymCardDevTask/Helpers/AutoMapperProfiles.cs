using AutoMapper;
using VirtuagymDevTask.DTOs;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<InvoiceToUpdateDTO, Invoice>();
            CreateMap<MembershipToUpdateDTO, Membership>();

            CreateMap<Invoice, InvoiceToUpdateDTO>();
            CreateMap<Invoice, InvoiceToCreateDTO>();
            CreateMap<InvoiceToCreateDTO, Invoice>(); 
            CreateMap<InvoiceLineDTO, InvoiceLines>();
            CreateMap<InvoiceLines, InvoiceLineDTO>();
        }
    }
}
