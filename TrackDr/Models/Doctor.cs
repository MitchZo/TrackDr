﻿using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class Rootobject
    {
        public Rootobject()
        {
            UserDoctorNavigation = new HashSet<UserDoctor>();
        }

        public string Id { get; set; }
        public int? UserDoctorId { get; set; }

        public virtual UserDoctor UserDoctor { get; set; }
        public virtual ICollection<UserDoctor> UserDoctorNavigation { get; set; }
    }
}
