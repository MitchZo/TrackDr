using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class UserDoctor
    {
        public UserDoctor()
        {
            Doctor = new HashSet<Doctor>();
            TrackDrUser = new HashSet<TrackDrUser>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string DoctorId { get; set; }

        public virtual Doctor DoctorNavigation { get; set; }
        public virtual TrackDrUser User { get; set; }
        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<TrackDrUser> TrackDrUser { get; set; }
    }
}
