namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_other_app
    {
        public int id { get; set; }

        [StringLength(1024)]
        public string name { get; set; }

        public string google_play { get; set; }

        public string app_store { get; set; }

        public DateTime? time_created { get; set; }

        public DateTime? time_last_update { get; set; }
    }
}
