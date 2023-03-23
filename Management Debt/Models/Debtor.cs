namespace Management_Debt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Debtor")]
    public partial class Debtor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Debtor()
        {
            DebitNote = new HashSet<DebitNote>();
            DebtorUser = new HashSet<DebtorUser>();
        }

        [Key]
        [StringLength(50)]
        public string did { get; set; }

        [Required]
        [StringLength(50)]
        public string address { get; set; }

        [StringLength(50)]
        public string birthday { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public int? status { get; set; }

        [StringLength(50)]
        public string full_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DebitNote> DebitNote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DebtorUser> DebtorUser { get; set; }
    }
}
