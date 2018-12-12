namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_daily
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_daily()
        {
            preg_daily_like = new HashSet<preg_daily_like>();
        }

        public int id { get; set; }

        public int? daily_type_id { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        [StringLength(1024)]
        public string hingline_image { get; set; }

        [StringLength(1024)]
        public string short_description { get; set; }

        [StringLength(1024)]
        public string description { get; set; }

        [StringLength(1024)]
        public string daily_relation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_daily_like> preg_daily_like { get; set; }

        public virtual preg_daily_type preg_daily_type { get; set; }
    }
}
