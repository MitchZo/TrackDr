using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class Child
    {
        public Child()
        {
            ChildDoctor = new HashSet<ChildDoctor>();
        }

        public int ChildId { get; set; }
        public string ParentId { get; set; }

        public virtual Parent Parent { get; set; }
        public virtual ICollection<ChildDoctor> ChildDoctor { get; set; }
    }
}
