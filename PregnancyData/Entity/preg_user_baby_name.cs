namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_user_baby_name
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int baby_name_id { get; set; }

        public virtual preg_baby_name preg_baby_name { get; set; }

        public virtual preg_baby_name preg_baby_name1 { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
