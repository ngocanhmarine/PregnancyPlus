namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_question()
        {
            preg_answer = new HashSet<preg_answer>();
        }

        public int id { get; set; }

        public int? question_type_id { get; set; }

        [StringLength(1024)]
        public string content { get; set; }

        public int? custom_question_by_user_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_answer> preg_answer { get; set; }

        public virtual preg_question_type preg_question_type { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
