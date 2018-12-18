namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_baby_name
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_baby_name()
        {
            preg_user_baby_name = new HashSet<preg_user_baby_name>();
            preg_user_baby_name1 = new HashSet<preg_user_baby_name>();
        }

        public int id { get; set; }

        public int? country_id { get; set; }

        public int? gender_id { get; set; }

        [StringLength(1024)]
        public string name { get; set; }

        public int? custom_baby_name_by_user_id { get; set; }

        public virtual preg_gender preg_gender { get; set; }

        public virtual preg_country preg_country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_user_baby_name> preg_user_baby_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_user_baby_name> preg_user_baby_name1 { get; set; }
    }
}
