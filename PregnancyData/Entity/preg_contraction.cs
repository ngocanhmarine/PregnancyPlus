namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_contraction
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        public DateTime? date_time { get; set; }

        public DateTime? duration { get; set; }

        public DateTime? interval { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
