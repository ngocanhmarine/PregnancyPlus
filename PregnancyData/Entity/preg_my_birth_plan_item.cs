namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_my_birth_plan_item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_my_birth_plan_item()
        {
            preg_my_birth_plan = new HashSet<preg_my_birth_plan>();
            preg_my_birth_plan1 = new HashSet<preg_my_birth_plan>();
        }

        public int id { get; set; }

        public int? my_birth_plan_type_id { get; set; }

        [StringLength(1024)]
        public string item_content { get; set; }

        public int? custom_item_by_user_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_my_birth_plan> preg_my_birth_plan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_my_birth_plan> preg_my_birth_plan1 { get; set; }

        public virtual preg_my_birth_plan_type preg_my_birth_plan_type { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
