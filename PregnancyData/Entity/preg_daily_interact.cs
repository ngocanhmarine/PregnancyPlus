namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_daily_interact
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int daily_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        public int? like { get; set; }

        [StringLength(1024)]
        public string comment { get; set; }

        public int? share { get; set; }

        public int? notification { get; set; }

        public int? status { get; set; }

        public virtual preg_daily preg_daily { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
