using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class UserDoctor
    {
        public UserDoctor()
        {
            DoctorUid = new HashSet<DoctorUid>();
            TrackDrUser = new HashSet<TrackDrUser>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string DoctorId { get; set; }

        public virtual DoctorUid Doctor { get; set; }
        public virtual TrackDrUser User { get; set; }
        public virtual ICollection<DoctorUid> DoctorUid { get; set; }
        public virtual ICollection<TrackDrUser> TrackDrUser { get; set; }
    }
}
