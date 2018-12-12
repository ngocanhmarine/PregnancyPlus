namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_baby_name
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        public int? preg_country_id { get; set; }

        public int? preg_gender_id { get; set; }

        [StringLength(1024)]
        public string name { get; set; }

        public virtual preg_gender preg_gender { get; set; }

        public virtual preg_country preg_country { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
