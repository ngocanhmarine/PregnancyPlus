namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_my_birth_plan_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_my_birth_plan_type()
        {
            preg_my_birth_plan_item = new HashSet<preg_my_birth_plan_item>();
        }

        public int id { get; set; }

        [StringLength(1024)]
        public string type { get; set; }

        [StringLength(1024)]
        public string type_icon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_my_birth_plan_item> preg_my_birth_plan_item { get; set; }
    }
}
