namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_my_weight_in_st
    {
        public int id { get; set; }

        public int? position { get; set; }

        public int? value { get; set; }
    }
}
