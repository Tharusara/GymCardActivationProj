using System;
using System.ComponentModel.DataAnnotations;

namespace VirtuagymDevTask.Models
{
    public class Membership
    {
        [Required]
        public int Id { get; set; }
        public MembershipStatus Status { get; set; }
        public MembershipType MembershipType { get; set; }
        public int Credits { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public User User { get; set; }
        [Required]
        public int UserId { get; set; }

    }

    public enum MembershipStatus
    {
        Cancelled = 0,
        Active = 1,
    }

    public enum MembershipType
    {
        Guest = 0,
        Member = 1,
    }
}
