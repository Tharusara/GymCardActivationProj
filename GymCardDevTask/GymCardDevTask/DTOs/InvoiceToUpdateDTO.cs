using System;
using System.ComponentModel.DataAnnotations;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.DTOs
{
    public class InvoiceToUpdateDTO
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Month { get; set; }
        public InvoiceStatus Status { get; set; }

        [StringLength(65535, ErrorMessage = "Maximum character count is 65535")]
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
