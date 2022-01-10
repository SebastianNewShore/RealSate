namespace ApiRealState.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbRegion")]
    public partial class tbRegion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbRegion()
        {
            tbCities = new HashSet<tbCity>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public byte Enabled { get; set; }

        public int IdCountry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCity> tbCities { get; set; }

        public virtual tbCountry tbCountry { get; set; }
    }
}
