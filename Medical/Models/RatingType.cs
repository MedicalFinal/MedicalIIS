using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class RatingType
    {
        public RatingType()
        {
            RatingDoctors = new HashSet<RatingDoctor>();
            Reviews = new HashSet<Review>();
        }

        public int RatingTypeId { get; set; }
        public string RatingTypeName { get; set; }

        public virtual ICollection<RatingDoctor> RatingDoctors { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
