namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_my_belly
    {
        public int id { get; set; }

        [StringLength(1024)]
        public string image { get; set; }

        public int? my_belly_type_id { get; set; }

        public int? month { get; set; }

        public int? user_id { get; set; }

        public virtual preg_my_belly_type preg_my_belly_type { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
