namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_time_line
    {
        public int id { get; set; }

        public int? week_id { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        [StringLength(1024)]
        public string image { get; set; }

        [StringLength(1024)]
        public string position { get; set; }

        public int? time_line_id { get; set; }

        public virtual preg_time_frame preg_time_frame { get; set; }

        public virtual preg_week preg_week { get; set; }
    }
}
