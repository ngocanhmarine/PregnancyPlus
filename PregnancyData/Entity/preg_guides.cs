namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_guides
    {
        public int id { get; set; }

        public int? page_id { get; set; }

        public int? guides_type_id { get; set; }

        public virtual preg_guides_type preg_guides_type { get; set; }

        public virtual preg_page preg_page { get; set; }
    }
}
