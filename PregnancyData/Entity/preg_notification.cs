namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_notification
    {
        public int id { get; set; }

        public int? week_id { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        public string content { get; set; }

        public DateTime? time_created { get; set; }

        public DateTime? time_last_push { get; set; }

        public virtual preg_week preg_week { get; set; }
    }
}
