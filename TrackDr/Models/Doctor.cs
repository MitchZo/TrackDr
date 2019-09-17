using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            ChildDoctor = new HashSet<ChildDoctor>();
            ParentDoctor = new HashSet<ParentDoctor>();
        }

        public string DoctorId { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<ChildDoctor> ChildDoctor { get; set; }
        public virtual ICollection<ParentDoctor> ParentDoctor { get; set; }
    }
}
