namespace ApiRealState.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbEmployee")]
    public partial class tbEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEmployee()
        {
            tbPropertyTraces = new HashSet<tbPropertyTrace>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string DocumentNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(20)]
        public string CellPhone { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        public byte Enabled { get; set; }

        public int IdDocument { get; set; }

        public int IdPosition { get; set; }

        public int IdGender { get; set; }

        public int IdCity { get; set; }

        public virtual tbCity tbCity { get; set; }

        public virtual tbDocument tbDocument { get; set; }

        public virtual tbGender tbGender { get; set; }

        public virtual tbPosition tbPosition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPropertyTrace> tbPropertyTraces { get; set; }
    }
}
