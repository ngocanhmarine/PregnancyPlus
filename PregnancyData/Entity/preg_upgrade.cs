namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_upgrade
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        [StringLength(200)]
        public string version { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
