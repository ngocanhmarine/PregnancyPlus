namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_todo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_todo()
        {
            preg_user_todo = new HashSet<preg_user_todo>();
        }

        public int id { get; set; }

        public int? day_id { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        public int? custom_task_by_user_id { get; set; }

        public virtual preg_daily preg_daily { get; set; }

        public virtual preg_user preg_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_user_todo> preg_user_todo { get; set; }
    }
}
