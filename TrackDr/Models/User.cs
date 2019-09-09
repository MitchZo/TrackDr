using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class User
    {
        public User()
        {
            UserDoctor = new HashSet<UserDoctor>();
        }

        public string Id { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers UserNavigation { get; set; }
        public virtual ICollection<UserDoctor> UserDoctor { get; set; }
    }
}
