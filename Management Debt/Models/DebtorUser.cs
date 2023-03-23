namespace Management_Debt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DebtorUser")]
    public partial class DebtorUser
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string did { get; set; }

        [Required]
        [StringLength(50)]
        public string uid { get; set; }

        public virtual Debtor Debtor { get; set; }

        public virtual User User { get; set; }
    }
}
