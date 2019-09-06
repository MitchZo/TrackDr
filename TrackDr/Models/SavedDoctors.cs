using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class SavedDoctors
    {
        public int Id { get; set; }
        public string DrList { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
