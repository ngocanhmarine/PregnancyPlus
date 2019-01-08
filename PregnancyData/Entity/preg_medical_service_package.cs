namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_medical_service_package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_medical_service_package()
        {
            preg_medical_package_test = new HashSet<preg_medical_package_test>();
            preg_medical_package_test1 = new HashSet<preg_medical_package_test>();
            preg_user_medical_service_package = new HashSet<preg_user_medical_service_package>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        [StringLength(1024)]
        public string description { get; set; }

        [StringLength(1024)]
        public string content { get; set; }

        public double? discount { get; set; }

        [StringLength(1024)]
        public string execution_department { get; set; }

        [StringLength(1024)]
        public string address { get; set; }

        public DateTime? execution_time { get; set; }

        public int? place { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_medical_package_test> preg_medical_package_test { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_medical_package_test> preg_medical_package_test1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_user_medical_service_package> preg_user_medical_service_package { get; set; }
    }
}
