namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ratings
    {
        [Key]
        public int RatingId { get; set; }

        public string UserName { get; set; }

        public int PhotoId { get; set; }

        public virtual Photos Photos { get; set; }
    }
}
