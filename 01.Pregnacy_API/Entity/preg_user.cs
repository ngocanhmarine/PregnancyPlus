namespace _01.Pregnacy_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_user
    {
        public int ID { get; set; }

        [StringLength(10)]
        public string password { get; set; }

        [StringLength(10)]
        public string phone { get; set; }

        [StringLength(10)]
        public string social_type { get; set; }

        [StringLength(10)]
        public string first_name { get; set; }

        [StringLength(10)]
        public string last_name { get; set; }

        [StringLength(10)]
        public string you_are_the { get; set; }

        [StringLength(10)]
        public string location { get; set; }

        [StringLength(10)]
        public string status { get; set; }

        [StringLength(10)]
        public string avarta { get; set; }
    }
}
