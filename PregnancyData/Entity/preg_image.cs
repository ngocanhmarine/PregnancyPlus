namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_image
    {
        public int id { get; set; }

        public int? image_type_id { get; set; }

        [StringLength(1024)]
        public string image { get; set; }

        public int? week_id { get; set; }

        public virtual preg_image_type preg_image_type { get; set; }

        public virtual preg_week preg_week { get; set; }
    }
}
