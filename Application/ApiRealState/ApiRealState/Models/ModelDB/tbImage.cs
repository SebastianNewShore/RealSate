namespace ApiRealState.Models.ModelDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbImage")]
    public partial class tbImage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string File { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public byte Enabled { get; set; }

        public int IdProperty { get; set; }

        public virtual tbProperty tbProperty { get; set; }
    }
}
