namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_medical_package_test
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int medical_service_package_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int medical_test_id { get; set; }

        public virtual preg_medical_service_package preg_medical_service_package { get; set; }

        public virtual preg_medical_service_package preg_medical_service_package1 { get; set; }

        public virtual preg_medical_test preg_medical_test { get; set; }
    }
}
