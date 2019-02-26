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
			preg_contact_us = new HashSet<preg_contact_us>();
			preg_contraction = new HashSet<preg_contraction>();
			preg_customer_response = new HashSet<preg_customer_response>();
			preg_customer_response1 = new HashSet<preg_customer_response>();
			preg_daily_interact = new HashSet<preg_daily_interact>();
			preg_hospital_bag_item = new HashSet<preg_hospital_bag_item>();
			preg_my_belly = new HashSet<preg_my_belly>();
			preg_my_birth_plan = new HashSet<preg_my_birth_plan>();
			preg_my_birth_plan_item = new HashSet<preg_my_birth_plan_item>();
			preg_my_weight = new HashSet<preg_my_weight>();
			preg_phone = new HashSet<preg_phone>();
			preg_pregnancy = new HashSet<preg_pregnancy>();
			preg_profession = new HashSet<preg_profession>();
			preg_question = new HashSet<preg_question>();
			preg_setting = new HashSet<preg_setting>();
			preg_shopping_item = new HashSet<preg_shopping_item>();
			preg_todo = new HashSet<preg_todo>();
			preg_upgrade = new HashSet<preg_upgrade>();
			preg_weekly_interact = new HashSet<preg_weekly_interact>();
			preg_user_baby_name = new HashSet<preg_user_baby_name>();
			preg_user_hospital_bag_item = new HashSet<preg_user_hospital_bag_item>();
			preg_user_kick_history = new HashSet<preg_user_kick_history>();
			preg_user_medical_service_package = new HashSet<preg_user_medical_service_package>();
			preg_user_shopping_cart = new HashSet<preg_user_shopping_cart>();
			preg_user_todo = new HashSet<preg_user_todo>();
		}

		public int id { get; set; }

		[StringLength(1024)]
		public string password { get; set; }

		[StringLength(1024)]
		public string phone { get; set; }

		public int? social_type_id { get; set; }

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
		public string avatar { get; set; }

		[StringLength(1024)]
		public string email { get; set; }

		[StringLength(50)]
		public string uid { get; set; }

		public DateTime? time_created { get; set; }

		public DateTime? time_last_login { get; set; }

		public int? nopcustomer_id { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_answer> preg_answer { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_appointment> preg_appointment { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_auth> preg_auth { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_contact_us> preg_contact_us { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_contraction> preg_contraction { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_customer_response> preg_customer_response { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_customer_response> preg_customer_response1 { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_daily_interact> preg_daily_interact { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_hospital_bag_item> preg_hospital_bag_item { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_my_belly> preg_my_belly { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_my_birth_plan> preg_my_birth_plan { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_my_birth_plan_item> preg_my_birth_plan_item { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_my_weight> preg_my_weight { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_phone> preg_phone { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_pregnancy> preg_pregnancy { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_profession> preg_profession { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_question> preg_question { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_setting> preg_setting { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_shopping_item> preg_shopping_item { get; set; }

		public virtual preg_social_type preg_social_type { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_todo> preg_todo { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_upgrade> preg_upgrade { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_weekly_interact> preg_weekly_interact { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_user_baby_name> preg_user_baby_name { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_user_hospital_bag_item> preg_user_hospital_bag_item { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_user_kick_history> preg_user_kick_history { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_user_medical_service_package> preg_user_medical_service_package { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_user_shopping_cart> preg_user_shopping_cart { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<preg_user_todo> preg_user_todo { get; set; }
	}
}
