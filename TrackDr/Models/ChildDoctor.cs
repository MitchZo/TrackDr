using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class ChildDoctor
    {
        public string ChildDoctorId { get; set; }
        public string DoctorId { get; set; }
        public int? ChildId { get; set; }

        public virtual Child Child { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
