namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_setting
    {
        public int id { get; set; }

        public bool? reminders { get; set; }

        public bool? length_units { get; set; }

        public int? weight_unit { get; set; }

        public int? user_id { get; set; }

        public int? revoke_consent { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
