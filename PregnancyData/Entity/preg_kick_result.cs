namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_kick_result
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_kick_result()
        {
            preg_user_kick_history = new HashSet<preg_user_kick_history>();
        }

        public int id { get; set; }

        public int kick_order { get; set; }

        public DateTime? kick_time { get; set; }

        public DateTime? elapsed_time { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_user_kick_history> preg_user_kick_history { get; set; }
    }
}
