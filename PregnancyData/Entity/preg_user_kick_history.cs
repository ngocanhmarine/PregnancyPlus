namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_user_kick_history
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int kick_result_id { get; set; }

        public DateTime? kick_date { get; set; }

        public DateTime? duration { get; set; }

        public virtual preg_kick_result preg_kick_result { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
