using System;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.DTOs
{
    public class MembershipToUpdateDTO
    {
        public int Id { get; set; }
        public MembershipStatus Status { get; set; }
        public MembershipType MembershipType { get; set; }
        public int Credits { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int UserId { get; set; }
    }
}
