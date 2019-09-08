using System;
using System.Collections.Generic;

namespace TrackDr.Models
{
    public partial class UserChild
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string ConditionList { get; set; }
        public string ChildsDoctor { get; set; }
        public int? ParentId { get; set; }

        public virtual TrackDrUser Parent { get; set; }
    }
}
