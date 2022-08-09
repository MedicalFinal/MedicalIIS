using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class State
    {
        public State()
        {
            Reserves = new HashSet<Reserve>();
        }

        public int StateId { get; set; }
        public string State1 { get; set; }

        public virtual ICollection<Reserve> Reserves { get; set; }
    }
}
