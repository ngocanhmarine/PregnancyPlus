namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_answer
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int question_id { get; set; }

        public DateTime? questiondate { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        public string content { get; set; }

        public virtual preg_question preg_question { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
