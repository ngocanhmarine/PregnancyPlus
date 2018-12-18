namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_pregnancy
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        public int? baby_gender { get; set; }

        public DateTime? due_date { get; set; }

        public int? show_week { get; set; }

        public int? pregnancy_loss { get; set; }

        public int? baby_already_born { get; set; }

        public DateTime? date_of_birth { get; set; }

        public int? weeks_pregnant { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
