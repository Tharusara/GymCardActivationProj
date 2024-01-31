using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtuagymDevTask.Models
{
    public class InvoiceLines
    {
        [Required]
        public int Id { get; set; }

        [StringLength(65535, ErrorMessage = "Maximum character count is 65535")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public Invoice Invoice { get; set; }
        public int? InvoiceId { get; set; }
    }
}
