namespace EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int OrderId { get; set; }

        public int CustId { get; set; }

        [Column("CarId")]
        public int CarId { get; set; }
        //[ForeignKey(nameof(Foo))]

        public virtual Customer Customer { get; set; }

        public virtual Car Car { get; set; }
    }
}
