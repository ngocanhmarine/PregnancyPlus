namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_faq_answer
    {
        public int id { get; set; }

        public int faq_id { get; set; }

        [StringLength(1024)]
        public string answer_content { get; set; }

        public virtual preg_faq preg_faq { get; set; }
    }
}
