namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_weekly_interact
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int week_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        public int? like { get; set; }

        public string comment { get; set; }

        [StringLength(1024)]
        public string photo { get; set; }

        [StringLength(1024)]
        public string share { get; set; }

        public int? notification { get; set; }

        public int? status { get; set; }

        public virtual preg_user preg_user { get; set; }

        public virtual preg_week preg_week { get; set; }
    }
}
