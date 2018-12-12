namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_answer
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        public DateTime? answerdate { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        [StringLength(1024)]
        public string content { get; set; }

        public int? question_id { get; set; }

        public virtual preg_question preg_question { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
