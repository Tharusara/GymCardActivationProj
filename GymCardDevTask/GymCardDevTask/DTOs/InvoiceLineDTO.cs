using System.ComponentModel.DataAnnotations;

namespace VirtuagymDevTask.DTOs
{
    public class InvoiceLineDTO
    {
        [StringLength(65535, ErrorMessage = "Maximum character count is 65535")]
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
