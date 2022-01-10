namespace ApiRealState.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbPropertyTrace")]
    public partial class tbPropertyTrace
    {
        public int Id { get; set; }

        [Column(TypeName = "money")]
        public decimal? Value { get; set; }

        public decimal? Tax { get; set; }

        public int IdStateProperty { get; set; }

        public int? IdEmployee { get; set; }

        public int IdCustomer { get; set; }

        public int IdProperty { get; set; }

        public virtual tbCustomer tbCustomer { get; set; }

        public virtual tbEmployee tbEmployee { get; set; }

        public virtual tbProperty tbProperty { get; set; }

        public virtual tbStateProperty tbStateProperty { get; set; }
    }
}
