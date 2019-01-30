namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_week
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_week()
        {
            preg_image = new HashSet<preg_image>();
            preg_my_weight = new HashSet<preg_my_weight>();
            preg_notification = new HashSet<preg_notification>();
            preg_size_guide = new HashSet<preg_size_guide>();
            preg_time_line = new HashSet<preg_time_line>();
            preg_weekly_interact = new HashSet<preg_weekly_interact>();
        }

        public int id { get; set; }

        public double? length { get; set; }

        public double? weight { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        [StringLength(1024)]
        public string highline_image { get; set; }

        [StringLength(1024)]
        public string short_description { get; set; }

        [StringLength(1024)]
        public string description { get; set; }

        [StringLength(1024)]
        public string daily_relation { get; set; }

        public string meta_description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_image> preg_image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_my_weight> preg_my_weight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_notification> preg_notification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_size_guide> preg_size_guide { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_time_line> preg_time_line { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_weekly_interact> preg_weekly_interact { get; set; }
    }
}
