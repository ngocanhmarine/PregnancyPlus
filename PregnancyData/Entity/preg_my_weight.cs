namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_my_weight
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        public int? my_weight_type_id { get; set; }

        public DateTime? start_date { get; set; }

        public double? pre_pregnancy_weight { get; set; }

        public double? current_weight { get; set; }

        public int? week_id { get; set; }

        public DateTime? current_date { get; set; }

        public virtual preg_my_weight_unit preg_my_weight_unit { get; set; }

        public virtual preg_user preg_user { get; set; }

        public virtual preg_week preg_week { get; set; }
    }
}
