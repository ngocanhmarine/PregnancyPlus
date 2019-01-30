namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_phone
    {
        public int id { get; set; }

        public int? profession_id { get; set; }

        [StringLength(1024)]
        public string phone_number { get; set; }

        public int? user_id { get; set; }

        [StringLength(1024)]
        public string name { get; set; }

        public virtual preg_profession_type preg_profession_type { get; set; }

        public virtual preg_user preg_user { get; set; }
    }
}
