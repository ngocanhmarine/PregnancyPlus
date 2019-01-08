namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_customer_response
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [StringLength(1024)]
        public string content { get; set; }

        public DateTime? time { get; set; }

        public int? answer_user_id { get; set; }

        public DateTime? answer_date { get; set; }

        [StringLength(1024)]
        public string answer_content { get; set; }

        public virtual preg_user preg_user { get; set; }

        public virtual preg_user preg_user1 { get; set; }
    }
}
