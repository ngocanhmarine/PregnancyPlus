namespace PregnancyData.Entity
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class PregnancyEntity : DbContext
	{
		public PregnancyEntity()
			: base("name=PregnancyEntity")
		{
		}

		public virtual DbSet<preg_answer> preg_answer { get; set; }
		public virtual DbSet<preg_appointment> preg_appointment { get; set; }
		public virtual DbSet<preg_appointment_measurement> preg_appointment_measurement { get; set; }
		public virtual DbSet<preg_appointment_type> preg_appointment_type { get; set; }
		public virtual DbSet<preg_auth> preg_auth { get; set; }
		public virtual DbSet<preg_baby_name> preg_baby_name { get; set; }
		public virtual DbSet<preg_contact_us> preg_contact_us { get; set; }
		public virtual DbSet<preg_contraction> preg_contraction { get; set; }
		public virtual DbSet<preg_country> preg_country { get; set; }
		public virtual DbSet<preg_customer_response> preg_customer_response { get; set; }
		public virtual DbSet<preg_daily> preg_daily { get; set; }
		public virtual DbSet<preg_daily_interact> preg_daily_interact { get; set; }
		public virtual DbSet<preg_faq> preg_faq { get; set; }
		public virtual DbSet<preg_faq_answer> preg_faq_answer { get; set; }
		public virtual DbSet<preg_gender> preg_gender { get; set; }
		public virtual DbSet<preg_guides> preg_guides { get; set; }
		public virtual DbSet<preg_guides_type> preg_guides_type { get; set; }
		public virtual DbSet<preg_help> preg_help { get; set; }
		public virtual DbSet<preg_help_category> preg_help_category { get; set; }
		public virtual DbSet<preg_hospital_bag_item> preg_hospital_bag_item { get; set; }
		public virtual DbSet<preg_image> preg_image { get; set; }
		public virtual DbSet<preg_image_type> preg_image_type { get; set; }
		public virtual DbSet<preg_kick_result> preg_kick_result { get; set; }
		public virtual DbSet<preg_medical_package_test> preg_medical_package_test { get; set; }
		public virtual DbSet<preg_medical_service_package> preg_medical_service_package { get; set; }
		public virtual DbSet<preg_medical_test> preg_medical_test { get; set; }
		public virtual DbSet<preg_my_belly> preg_my_belly { get; set; }
		public virtual DbSet<preg_my_birth_plan> preg_my_birth_plan { get; set; }
		public virtual DbSet<preg_my_birth_plan_item> preg_my_birth_plan_item { get; set; }
		public virtual DbSet<preg_my_birth_plan_type> preg_my_birth_plan_type { get; set; }
		public virtual DbSet<preg_my_weight> preg_my_weight { get; set; }
		public virtual DbSet<preg_my_weight_unit> preg_my_weight_unit { get; set; }
		public virtual DbSet<preg_notification> preg_notification { get; set; }
		public virtual DbSet<preg_other_app> preg_other_app { get; set; }
		public virtual DbSet<preg_page> preg_page { get; set; }
		public virtual DbSet<preg_phone> preg_phone { get; set; }
		public virtual DbSet<preg_pregnancy> preg_pregnancy { get; set; }
		public virtual DbSet<preg_profession> preg_profession { get; set; }
		public virtual DbSet<preg_profession_type> preg_profession_type { get; set; }
		public virtual DbSet<preg_question> preg_question { get; set; }
		public virtual DbSet<preg_question_type> preg_question_type { get; set; }
		public virtual DbSet<preg_setting> preg_setting { get; set; }
		public virtual DbSet<preg_shopping_category> preg_shopping_category { get; set; }
		public virtual DbSet<preg_shopping_item> preg_shopping_item { get; set; }
		public virtual DbSet<preg_size_guide> preg_size_guide { get; set; }
		public virtual DbSet<preg_social_type> preg_social_type { get; set; }
		public virtual DbSet<preg_time_frame> preg_time_frame { get; set; }
		public virtual DbSet<preg_time_line> preg_time_line { get; set; }
		public virtual DbSet<preg_todo> preg_todo { get; set; }
		public virtual DbSet<preg_upgrade> preg_upgrade { get; set; }
		public virtual DbSet<preg_user> preg_user { get; set; }
		public virtual DbSet<preg_user_baby_name> preg_user_baby_name { get; set; }
		public virtual DbSet<preg_user_hospital_bag_item> preg_user_hospital_bag_item { get; set; }
		public virtual DbSet<preg_user_kick_history> preg_user_kick_history { get; set; }
		public virtual DbSet<preg_user_medical_service_package> preg_user_medical_service_package { get; set; }
		public virtual DbSet<preg_user_shopping_cart> preg_user_shopping_cart { get; set; }
		public virtual DbSet<preg_user_todo> preg_user_todo { get; set; }
		public virtual DbSet<preg_week> preg_week { get; set; }
		public virtual DbSet<preg_weekly_interact> preg_weekly_interact { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<preg_answer>()
				.Property(e => e.content)
				.IsUnicode(false);

			modelBuilder.Entity<preg_appointment>()
				.HasMany(e => e.preg_appointment_measurement)
				.WithOptional(e => e.preg_appointment)
				.HasForeignKey(e => e.appointment_id);

			modelBuilder.Entity<preg_appointment_type>()
				.HasMany(e => e.preg_appointment)
				.WithOptional(e => e.preg_appointment_type)
				.HasForeignKey(e => e.appointment_type_id);

			modelBuilder.Entity<preg_auth>()
				.Property(e => e.token)
				.IsUnicode(false);

			modelBuilder.Entity<preg_auth>()
				.Property(e => e.valid_to)
				.IsUnicode(false);

			modelBuilder.Entity<preg_baby_name>()
				.HasMany(e => e.preg_user_baby_name)
				.WithRequired(e => e.preg_baby_name)
				.HasForeignKey(e => e.baby_name_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_baby_name>()
				.HasMany(e => e.preg_user_baby_name1)
				.WithRequired(e => e.preg_baby_name1)
				.HasForeignKey(e => e.baby_name_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_contact_us>()
				.Property(e => e.email)
				.IsUnicode(false);

			modelBuilder.Entity<preg_country>()
				.HasMany(e => e.preg_baby_name)
				.WithOptional(e => e.preg_country)
				.HasForeignKey(e => e.country_id);

			modelBuilder.Entity<preg_daily>()
				.Property(e => e.highline_image)
				.IsUnicode(false);

			modelBuilder.Entity<preg_daily>()
				.HasMany(e => e.preg_daily_interact)
				.WithRequired(e => e.preg_daily)
				.HasForeignKey(e => e.daily_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_daily>()
				.HasMany(e => e.preg_todo)
				.WithOptional(e => e.preg_daily)
				.HasForeignKey(e => e.day_id);

			modelBuilder.Entity<preg_faq>()
				.HasMany(e => e.preg_faq_answer)
				.WithRequired(e => e.preg_faq)
				.HasForeignKey(e => e.faq_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_gender>()
				.HasMany(e => e.preg_baby_name)
				.WithOptional(e => e.preg_gender)
				.HasForeignKey(e => e.gender_id);

			modelBuilder.Entity<preg_gender>()
				.HasMany(e => e.preg_pregnancy)
				.WithOptional(e => e.preg_gender)
				.HasForeignKey(e => e.baby_gender);

			modelBuilder.Entity<preg_guides_type>()
				.HasMany(e => e.preg_guides)
				.WithOptional(e => e.preg_guides_type)
				.HasForeignKey(e => e.guides_type_id);

			modelBuilder.Entity<preg_help>()
				.Property(e => e.image)
				.IsUnicode(false);

			modelBuilder.Entity<preg_help_category>()
				.Property(e => e.highline_image)
				.IsUnicode(false);

			modelBuilder.Entity<preg_help_category>()
				.HasMany(e => e.preg_help)
				.WithOptional(e => e.preg_help_category)
				.HasForeignKey(e => e.help_category_id);

			modelBuilder.Entity<preg_hospital_bag_item>()
				.HasMany(e => e.preg_user_hospital_bag_item)
				.WithRequired(e => e.preg_hospital_bag_item)
				.HasForeignKey(e => e.hospital_bag_item_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_image>()
				.Property(e => e.image)
				.IsUnicode(false);

			modelBuilder.Entity<preg_image_type>()
				.HasMany(e => e.preg_image)
				.WithOptional(e => e.preg_image_type)
				.HasForeignKey(e => e.image_type_id);

			modelBuilder.Entity<preg_kick_result>()
				.HasMany(e => e.preg_user_kick_history)
				.WithRequired(e => e.preg_kick_result)
				.HasForeignKey(e => e.kick_result_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_medical_service_package>()
				.HasMany(e => e.preg_medical_package_test)
				.WithRequired(e => e.preg_medical_service_package)
				.HasForeignKey(e => e.medical_service_package_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_medical_service_package>()
				.HasMany(e => e.preg_medical_package_test1)
				.WithRequired(e => e.preg_medical_service_package1)
				.HasForeignKey(e => e.medical_service_package_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_medical_service_package>()
				.HasMany(e => e.preg_user_medical_service_package)
				.WithRequired(e => e.preg_medical_service_package)
				.HasForeignKey(e => e.medical_service_package_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_medical_test>()
				.HasMany(e => e.preg_medical_package_test)
				.WithRequired(e => e.preg_medical_test)
				.HasForeignKey(e => e.medical_test_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_my_belly>()
				.Property(e => e.image)
				.IsUnicode(false);

			modelBuilder.Entity<preg_my_birth_plan_item>()
				.HasMany(e => e.preg_my_birth_plan)
				.WithRequired(e => e.preg_my_birth_plan_item)
				.HasForeignKey(e => e.my_birth_plan_item_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_my_birth_plan_item>()
				.HasMany(e => e.preg_my_birth_plan1)
				.WithRequired(e => e.preg_my_birth_plan_item1)
				.HasForeignKey(e => e.my_birth_plan_item_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_my_birth_plan_type>()
				.HasMany(e => e.preg_my_birth_plan_item)
				.WithOptional(e => e.preg_my_birth_plan_type)
				.HasForeignKey(e => e.my_birth_plan_type_id);

			modelBuilder.Entity<preg_my_weight_unit>()
				.HasMany(e => e.preg_appointment)
				.WithOptional(e => e.preg_my_weight_unit)
				.HasForeignKey(e => e.my_weight_type_id);

			modelBuilder.Entity<preg_my_weight_unit>()
				.HasMany(e => e.preg_my_weight)
				.WithOptional(e => e.preg_my_weight_unit)
				.HasForeignKey(e => e.my_weight_type_id);

			modelBuilder.Entity<preg_notification>()
				.Property(e => e.title)
				.IsUnicode(false);

			modelBuilder.Entity<preg_notification>()
				.Property(e => e.content)
				.IsUnicode(false);

			modelBuilder.Entity<preg_other_app>()
				.Property(e => e.name)
				.IsUnicode(false);

			modelBuilder.Entity<preg_other_app>()
				.Property(e => e.google_play)
				.IsUnicode(false);

			modelBuilder.Entity<preg_other_app>()
				.Property(e => e.app_store)
				.IsUnicode(false);

			modelBuilder.Entity<preg_page>()
				.HasMany(e => e.preg_guides)
				.WithOptional(e => e.preg_page)
				.HasForeignKey(e => e.page_id);

			modelBuilder.Entity<preg_phone>()
				.Property(e => e.phone_number)
				.IsUnicode(false);

			modelBuilder.Entity<preg_profession_type>()
				.HasMany(e => e.preg_appointment)
				.WithOptional(e => e.preg_profession_type)
				.HasForeignKey(e => e.profession_id);

			modelBuilder.Entity<preg_profession_type>()
				.HasMany(e => e.preg_phone)
				.WithOptional(e => e.preg_profession_type)
				.HasForeignKey(e => e.profession_id);

			modelBuilder.Entity<preg_profession_type>()
				.HasMany(e => e.preg_profession)
				.WithRequired(e => e.preg_profession_type)
				.HasForeignKey(e => e.profession_type_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_question>()
				.HasMany(e => e.preg_answer)
				.WithRequired(e => e.preg_question)
				.HasForeignKey(e => e.question_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_question_type>()
				.HasMany(e => e.preg_question)
				.WithOptional(e => e.preg_question_type)
				.HasForeignKey(e => e.question_type_id);

			modelBuilder.Entity<preg_shopping_category>()
				.Property(e => e.icon)
				.IsUnicode(false);

			modelBuilder.Entity<preg_shopping_category>()
				.HasMany(e => e.preg_shopping_item)
				.WithOptional(e => e.preg_shopping_category)
				.HasForeignKey(e => e.category_id);

			modelBuilder.Entity<preg_shopping_item>()
				.HasMany(e => e.preg_user_shopping_cart)
				.WithRequired(e => e.preg_shopping_item)
				.HasForeignKey(e => e.shopping_item_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_size_guide>()
				.Property(e => e.image)
				.IsUnicode(false);

			modelBuilder.Entity<preg_social_type>()
				.HasMany(e => e.preg_user)
				.WithOptional(e => e.preg_social_type)
				.HasForeignKey(e => e.social_type_id);

			modelBuilder.Entity<preg_time_frame>()
				.HasMany(e => e.preg_time_line)
				.WithOptional(e => e.preg_time_frame)
				.HasForeignKey(e => e.time_frame_id);

			modelBuilder.Entity<preg_time_line>()
				.Property(e => e.image)
				.IsUnicode(false);

			modelBuilder.Entity<preg_todo>()
				.HasMany(e => e.preg_user_todo)
				.WithRequired(e => e.preg_todo)
				.HasForeignKey(e => e.todo_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_upgrade>()
				.Property(e => e.version)
				.IsUnicode(false);

			modelBuilder.Entity<preg_user>()
				.Property(e => e.password)
				.IsUnicode(false);

			modelBuilder.Entity<preg_user>()
				.Property(e => e.phone)
				.IsUnicode(false);

			modelBuilder.Entity<preg_user>()
				.Property(e => e.uid)
				.IsUnicode(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_answer)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_appointment)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_auth)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_contact_us)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_contraction)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_customer_response)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_customer_response1)
				.WithOptional(e => e.preg_user1)
				.HasForeignKey(e => e.answer_user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_daily_interact)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_hospital_bag_item)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.custom_item_by_user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_my_belly)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_my_birth_plan)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_my_birth_plan_item)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.custom_item_by_user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_my_weight)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_phone)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_pregnancy)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_profession)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_question)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.custom_question_by_user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_setting)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_shopping_item)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.custom_item_by_user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_todo)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.custom_task_by_user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_upgrade)
				.WithOptional(e => e.preg_user)
				.HasForeignKey(e => e.user_id);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_weekly_interact)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_user_baby_name)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_user_hospital_bag_item)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_user_kick_history)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_user_medical_service_package)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_user_shopping_cart)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_user>()
				.HasMany(e => e.preg_user_todo)
				.WithRequired(e => e.preg_user)
				.HasForeignKey(e => e.user_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_week>()
				.HasMany(e => e.preg_image)
				.WithOptional(e => e.preg_week)
				.HasForeignKey(e => e.week_id);

			modelBuilder.Entity<preg_week>()
				.HasMany(e => e.preg_my_weight)
				.WithOptional(e => e.preg_week)
				.HasForeignKey(e => e.week_id);

			modelBuilder.Entity<preg_week>()
				.HasMany(e => e.preg_notification)
				.WithOptional(e => e.preg_week)
				.HasForeignKey(e => e.week_id);

			modelBuilder.Entity<preg_week>()
				.HasMany(e => e.preg_size_guide)
				.WithOptional(e => e.preg_week)
				.HasForeignKey(e => e.week_id);

			modelBuilder.Entity<preg_week>()
				.HasMany(e => e.preg_time_line)
				.WithOptional(e => e.preg_week)
				.HasForeignKey(e => e.week_id);

			modelBuilder.Entity<preg_week>()
				.HasMany(e => e.preg_weekly_interact)
				.WithRequired(e => e.preg_week)
				.HasForeignKey(e => e.week_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<preg_weekly_interact>()
				.Property(e => e.photo)
				.IsUnicode(false);
		}
	}
}
