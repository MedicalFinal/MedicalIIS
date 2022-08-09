using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Source
    {
        public Source()
        {
            Reserves = new HashSet<Reserve>();
        }

        public int SourceId { get; set; }
        public string Source1 { get; set; }

        public virtual ICollection<Reserve> Reserves { get; set; }
    }
}
