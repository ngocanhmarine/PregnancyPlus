namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_auth
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        [StringLength(1024)]
        public string token { get; set; }

        [StringLength(1024)]
        public string valid_to { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
