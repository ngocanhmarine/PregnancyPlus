namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_daily_like
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        public int? like_type_id { get; set; }

        public int? daily_id { get; set; }

        public virtual preg_daily preg_daily { get; set; }

        public virtual preg_like_type preg_like_type { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
