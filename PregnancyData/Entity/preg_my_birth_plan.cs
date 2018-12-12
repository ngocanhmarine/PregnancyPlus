namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_my_birth_plan
    {
        public int id { get; set; }

        public int? my_birth_plan_type_id { get; set; }

        public int? user_id { get; set; }

        [StringLength(1024)]
        public string icon { get; set; }

        [StringLength(1024)]
        public string title { get; set; }

        public virtual preg_my_birth_plan_type preg_my_birth_plan_type { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
