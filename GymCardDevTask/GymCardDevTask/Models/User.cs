using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtuagymDevTask.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        public string FullName { get; set; } 
        public DateTime DateOfBirth { get; set; } 
        public string CallingName { get; set; } 
        public int ContactNo { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Membership> Memberships { get; set; }
    }
}
