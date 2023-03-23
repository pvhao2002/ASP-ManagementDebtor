namespace Management_Debt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DebitNote")]
    public partial class DebitNote
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string did { get; set; }

        [Required]
        [StringLength(50)]
        public string uid { get; set; }

        [StringLength(200)]
        public string note { get; set; }

        public DateTime? pay_date { get; set; }

        public DateTime loan_date { get; set; }

        [Required]
        [StringLength(50)]
        public string payment_type { get; set; }

        [Column(TypeName = "money")]
        public decimal money { get; set; }

        public int? status { get; set; }

        public virtual Debtor Debtor { get; set; }

        public virtual User User { get; set; }
    }
}
