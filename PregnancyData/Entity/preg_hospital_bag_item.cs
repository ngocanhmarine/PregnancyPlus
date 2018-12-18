namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_hospital_bag_item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_hospital_bag_item()
        {
            preg_user_hospital_bag_item = new HashSet<preg_user_hospital_bag_item>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(1024)]
        public string name { get; set; }

        public int type { get; set; }

        public int? custom_item_by_user_id { get; set; }

        public virtual preg_user preg_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_user_hospital_bag_item> preg_user_hospital_bag_item { get; set; }
    }
}
