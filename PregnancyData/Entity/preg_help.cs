namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_help
    {
        public int id { get; set; }

        public int? help_category_id { get; set; }

        [StringLength(1024)]
        public string image { get; set; }

        public string description { get; set; }

        public virtual preg_help_category preg_help_category { get; set; }
    }
}
