namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_my_birth_plan
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int my_birth_plan_item_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        public virtual preg_my_birth_plan_item preg_my_birth_plan_item { get; set; }

        public virtual preg_my_birth_plan_item preg_my_birth_plan_item1 { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
