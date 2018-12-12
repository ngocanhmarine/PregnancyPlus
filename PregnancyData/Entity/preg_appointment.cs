namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_appointment
    {
        public int id { get; set; }

        [StringLength(1024)]
        public string name { get; set; }

        public int? profession_id { get; set; }

        public int? appointment_type_id { get; set; }

        public DateTime? appointment_date { get; set; }

        public DateTime? appointment_time { get; set; }

        public int? my_weight_type_id { get; set; }

        public double? weight_in_st { get; set; }

        public int? appointment_bp_dia_id { get; set; }

        public int? appointment_bp_sys_id { get; set; }

        public int? appointment_baby_heart_id { get; set; }

        public int? sync_to_heart { get; set; }

        [StringLength(1024)]
        public string note { get; set; }

        public int? user_id { get; set; }

        public virtual preg_appointment_baby_heart preg_appointment_baby_heart { get; set; }

        public virtual preg_appointment_bp_sys preg_appointment_bp_sys { get; set; }

        public virtual preg_appointment_bp_dia preg_appointment_bp_dia { get; set; }

        public virtual preg_appointment_type preg_appointment_type { get; set; }

        public virtual preg_my_weight_type preg_my_weight_type { get; set; }

        public virtual preg_profession preg_profession { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
