namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_user_medical_service_package
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int medical_service_package_id { get; set; }

        public DateTime? time { get; set; }

        [StringLength(1024)]
        public string description { get; set; }

        public double? total_cost { get; set; }

        public int? status { get; set; }

        [StringLength(1024)]
        public string payment_method { get; set; }

        public int? already_paid { get; set; }

        public virtual preg_medical_service_package preg_medical_service_package { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
