namespace ApiRealState.Models.ModelDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbProperty")]
    public partial class tbProperty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbProperty()
        {
            tbImages = new HashSet<tbImage>();
            tbPropertyTraces = new HashSet<tbPropertyTrace>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Adress { get; set; }

        [Column(TypeName = "money")]
        public decimal InternalValue { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime PublicationDate { get; set; }

        public int Area { get; set; }

        public int Rooms { get; set; }

        public int Bathrooms { get; set; }

        public int Garages { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateInformationUpdate { get; set; }

        public int IdOwner { get; set; }

        public int IdCategory { get; set; }

        public virtual tbCategory tbCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbImage> tbImages { get; set; }

        public virtual tbOwner tbOwner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPropertyTrace> tbPropertyTraces { get; set; }
    }
}
