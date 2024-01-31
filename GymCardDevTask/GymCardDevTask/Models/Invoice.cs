using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtuagymDevTask.Models
{
    public class Invoice
    {
        public Invoice()
        {
            DueDate = DateTime.Now;
        }

        [Required]
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Month { get; set; }
        public InvoiceStatus Status { get; set; }

        [StringLength(65535, ErrorMessage = "Maximum character count is 65535")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public User User { get; set; }
        [Required]
        public int UserId { get; set; }
        public ICollection<InvoiceLines> Lines { get; set; }
    }

    public enum InvoiceStatus
    {
        Outstanding = 0,
        Paid = 1,
        Void = 2,
    }
}
