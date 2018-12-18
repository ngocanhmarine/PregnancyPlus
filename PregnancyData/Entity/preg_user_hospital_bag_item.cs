namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_user_hospital_bag_item
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int hospital_bag_item_id { get; set; }

        public int? status { get; set; }

        public virtual preg_hospital_bag_item preg_hospital_bag_item { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
