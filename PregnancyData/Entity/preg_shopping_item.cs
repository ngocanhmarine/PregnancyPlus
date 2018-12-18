namespace PregnancyData.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class preg_shopping_item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preg_shopping_item()
        {
            preg_user_shopping_cart = new HashSet<preg_user_shopping_cart>();
        }

        public int id { get; set; }

        [StringLength(1024)]
        public string item_name { get; set; }

        public int? custom_item_by_user_id { get; set; }

        public int? category_id { get; set; }

        public int? status { get; set; }

        public virtual preg_shopping_category preg_shopping_category { get; set; }

        public virtual preg_user preg_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preg_user_shopping_cart> preg_user_shopping_cart { get; set; }
    }
}
