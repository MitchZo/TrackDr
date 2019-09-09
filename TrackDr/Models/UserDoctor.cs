using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class UserDoctor
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string UserId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual User User { get; set; }
    }
}
