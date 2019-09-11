using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Child = new HashSet<Child>();
            ParentDoctor = new HashSet<ParentDoctor>();
        }

        public string ParentId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Child> Child { get; set; }
        public virtual ICollection<ParentDoctor> ParentDoctor { get; set; }
    }
}
