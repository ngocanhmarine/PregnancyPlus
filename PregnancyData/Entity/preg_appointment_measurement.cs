namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_appointment_measurement
    {
        public int id { get; set; }

        public int? appointment_id { get; set; }

        public int? bp_dia { get; set; }

        public int? bp_sys { get; set; }

        public int? baby_heart { get; set; }

        public virtual preg_appointment preg_appointment { get; set; }
    }
}
