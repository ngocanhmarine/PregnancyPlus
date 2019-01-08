namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_size_guide
    {
        public int id { get; set; }

        [StringLength(1024)]
        public string image { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        [StringLength(1024)]
        public string description { get; set; }

        public int? week_id { get; set; }

        public double? length { get; set; }

        public double? weight { get; set; }

        public int? type { get; set; }

        public virtual preg_week preg_week { get; set; }
    }
}
