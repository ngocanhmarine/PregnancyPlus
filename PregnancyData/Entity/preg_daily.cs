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
            preg_daily_interact = new HashSet<preg_daily_interact>();
            preg_todo = new HashSet<preg_todo>();
        }

        public int id { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        [StringLength(1024)]
        public string highline_image { get; set; }

        [StringLength(1024)]
        public string short_description { get; set; }

        public string description { get; set; }

        [StringLength(1024)]
        public string daily_blog { get; set; }

        public string meta_description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_daily_interact> preg_daily_interact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_todo> preg_todo { get; set; }
    }
}
