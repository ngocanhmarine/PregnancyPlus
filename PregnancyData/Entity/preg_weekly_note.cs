namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_weekly_note
    {
        public int id { get; set; }

        public int? week_id { get; set; }

        public int? user_id { get; set; }

        [StringLength(1024)]
        public string photo { get; set; }

        [StringLength(1024)]
        public string note { get; set; }

        public int? like { get; set; }

        public virtual preg_user preg_user { get; set; }

        public virtual preg_week preg_week { get; set; }
    }
}
