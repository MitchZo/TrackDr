using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class TrackDrUser
    {
        public TrackDrUser()
        {
            UserChild = new HashSet<UserChild>();
            UserDoctorNavigation = new HashSet<UserDoctor>();
        }

        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int? UserDoctorId { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual UserDoctor UserDoctor { get; set; }
        public virtual ICollection<UserChild> UserChild { get; set; }
        public virtual ICollection<UserDoctor> UserDoctorNavigation { get; set; }
    }
}
