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
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
