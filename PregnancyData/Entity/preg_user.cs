namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_user()
        {
            preg_answer = new HashSet<preg_answer>();
            preg_appointment = new HashSet<preg_appointment>();
            preg_auth = new HashSet<preg_auth>();
            preg_baby_name = new HashSet<preg_baby_name>();
            preg_cotact_us = new HashSet<preg_cotact_us>();
            preg_daily_like = new HashSet<preg_daily_like>();
            preg_my_belly = new HashSet<preg_my_belly>();
            preg_my_birth_plan = new HashSet<preg_my_birth_plan>();
            preg_my_weight = new HashSet<preg_my_weight>();
            preg_phone = new HashSet<preg_phone>();
            preg_pregnancy = new HashSet<preg_pregnancy>();
            preg_question = new HashSet<preg_question>();
            preg_setting = new HashSet<preg_setting>();
            preg_todo = new HashSet<preg_todo>();
            preg_todo_other = new HashSet<preg_todo_other>();
            preg_upgrade = new HashSet<preg_upgrade>();
            preg_weekly_note = new HashSet<preg_weekly_note>();
        }

        public int id { get; set; }

        [StringLength(1024)]
        public string password { get; set; }

        [StringLength(1024)]
        public string phone { get; set; }

        [StringLength(1024)]
        public string social_type { get; set; }

        [StringLength(1024)]
        public string first_name { get; set; }

        [StringLength(1024)]
        public string last_name { get; set; }

        [StringLength(1024)]
        public string you_are_the { get; set; }

        [StringLength(1024)]
        public string location { get; set; }

        [StringLength(1024)]
        public string status { get; set; }

        [StringLength(1024)]
        public string avarta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_answer> preg_answer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_appointment> preg_appointment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_auth> preg_auth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_baby_name> preg_baby_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_cotact_us> preg_cotact_us { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_daily_like> preg_daily_like { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_my_belly> preg_my_belly { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_my_birth_plan> preg_my_birth_plan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_my_weight> preg_my_weight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_phone> preg_phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_pregnancy> preg_pregnancy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_question> preg_question { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_setting> preg_setting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_todo> preg_todo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_todo_other> preg_todo_other { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_upgrade> preg_upgrade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_weekly_note> preg_weekly_note { get; set; }
    }
}
