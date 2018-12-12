namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_todo
    {
        public int id { get; set; }

        public int? week_id { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        public int? user_id { get; set; }

        public virtual preg_user preg_user { get; set; }

        public virtual preg_week preg_week { get; set; }
    }
}
