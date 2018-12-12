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

        public virtual DbSet<preg_answer> preg_answers { get; set; }
        public virtual DbSet<preg_appointment> preg_appointments { get; set; }
        public virtual DbSet<preg_appointment_baby_heart> preg_appointment_baby_hearts { get; set; }
        public virtual DbSet<preg_appointment_bp_dia> preg_appointment_bp_dias { get; set; }
        public virtual DbSet<preg_appointment_bp_sys> preg_appointment_bp_syss { get; set; }
        public virtual DbSet<preg_appointment_type> preg_appointment_types { get; set; }
        public virtual DbSet<preg_auth> preg_auths { get; set; }
        public virtual DbSet<preg_baby_name> preg_baby_names { get; set; }
        public virtual DbSet<preg_cotact_us> preg_cotact_uss { get; set; }
        public virtual DbSet<preg_country> preg_countrys { get; set; }
        public virtual DbSet<preg_daily> preg_dailys { get; set; }
        public virtual DbSet<preg_daily_like> preg_daily_likes { get; set; }
        public virtual DbSet<preg_daily_type> preg_daily_types { get; set; }
        public virtual DbSet<preg_gender> preg_genders { get; set; }
        public virtual DbSet<preg_guides> preg_guidess { get; set; }
        public virtual DbSet<preg_guides_type> preg_guides_types { get; set; }
        public virtual DbSet<preg_help> preg_helps { get; set; }
        public virtual DbSet<preg_help_category> preg_help_categorys { get; set; }
        public virtual DbSet<preg_image> preg_images { get; set; }
        public virtual DbSet<preg_image_type> preg_image_types { get; set; }
        public virtual DbSet<preg_like_type> preg_like_types { get; set; }
        public virtual DbSet<preg_my_belly> preg_my_bellys { get; set; }
        public virtual DbSet<preg_my_belly_type> preg_my_belly_types { get; set; }
        public virtual DbSet<preg_my_birth_plan> preg_my_birth_plans { get; set; }
        public virtual DbSet<preg_my_birth_plan_type> preg_my_birth_plan_types { get; set; }
        public virtual DbSet<preg_my_weight> preg_my_weights { get; set; }
        public virtual DbSet<preg_my_weight_in_st> preg_my_weight_in_sts { get; set; }
        public virtual DbSet<preg_my_weight_type> preg_my_weight_types { get; set; }
        public virtual DbSet<preg_page> preg_pages { get; set; }
        public virtual DbSet<preg_phone> preg_phones { get; set; }
        public virtual DbSet<preg_pregnancy> preg_pregnancys { get; set; }
        public virtual DbSet<preg_profession> preg_professions { get; set; }
        public virtual DbSet<preg_question> preg_questions { get; set; }
        public virtual DbSet<preg_setting> preg_settings { get; set; }
        public virtual DbSet<preg_size_guide> preg_size_guides { get; set; }
        public virtual DbSet<preg_time_frame> preg_time_frames { get; set; }
        public virtual DbSet<preg_time_line> preg_time_lines { get; set; }
        public virtual DbSet<preg_todo> preg_todos { get; set; }
        public virtual DbSet<preg_todo_other> preg_todo_others { get; set; }
        public virtual DbSet<preg_upgrade> preg_upgrades { get; set; }
        public virtual DbSet<preg_user> preg_users { get; set; }
        public virtual DbSet<preg_week> preg_weeks { get; set; }
        public virtual DbSet<preg_weekly_note> preg_weekly_notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<preg_answer>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_answer>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<preg_appointment>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<preg_appointment>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<preg_appointment_baby_heart>()
                .HasMany(e => e.preg_appointment)
                .WithOptional(e => e.preg_appointment_baby_heart)
                .HasForeignKey(e => e.appointment_baby_heart_id);

            modelBuilder.Entity<preg_appointment_bp_dia>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<preg_appointment_bp_dia>()
                .HasMany(e => e.preg_appointment)
                .WithOptional(e => e.preg_appointment_bp_dia)
                .HasForeignKey(e => e.appointment_bp_dia_id);

            modelBuilder.Entity<preg_appointment_bp_sys>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<preg_appointment_bp_sys>()
                .HasMany(e => e.preg_appointment)
                .WithOptional(e => e.preg_appointment_bp_sys)
                .HasForeignKey(e => e.appointment_bp_sys_id);

            modelBuilder.Entity<preg_appointment_type>()
                .Property(e => e.type)
                .IsUnicode(false);

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
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<preg_cotact_us>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<preg_cotact_us>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<preg_country>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<preg_country>()
                .HasMany(e => e.preg_baby_name)
                .WithOptional(e => e.preg_country)
                .HasForeignKey(e => e.preg_country_id);

            modelBuilder.Entity<preg_daily>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_daily>()
                .Property(e => e.hingline_image)
                .IsUnicode(false);

            modelBuilder.Entity<preg_daily>()
                .Property(e => e.short_description)
                .IsUnicode(false);

            modelBuilder.Entity<preg_daily>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<preg_daily>()
                .Property(e => e.daily_relation)
                .IsUnicode(false);

            modelBuilder.Entity<preg_daily>()
                .HasMany(e => e.preg_daily_like)
                .WithOptional(e => e.preg_daily)
                .HasForeignKey(e => e.daily_id);

            modelBuilder.Entity<preg_daily_type>()
                .HasMany(e => e.preg_daily)
                .WithOptional(e => e.preg_daily_type)
                .HasForeignKey(e => e.daily_type_id);

            modelBuilder.Entity<preg_gender>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<preg_gender>()
                .HasMany(e => e.preg_baby_name)
                .WithOptional(e => e.preg_gender)
                .HasForeignKey(e => e.preg_gender_id);

            modelBuilder.Entity<preg_guides_type>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<preg_guides_type>()
                .HasMany(e => e.preg_guides)
                .WithOptional(e => e.preg_guides_type)
                .HasForeignKey(e => e.guides_type_id);

            modelBuilder.Entity<preg_help>()
                .Property(e => e.hingline_image)
                .IsUnicode(false);

            modelBuilder.Entity<preg_help>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<preg_help_category>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<preg_help_category>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<preg_help_category>()
                .HasMany(e => e.preg_help)
                .WithOptional(e => e.preg_help_category)
                .HasForeignKey(e => e.help_category_id);

            modelBuilder.Entity<preg_image>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<preg_image_type>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<preg_image_type>()
                .HasMany(e => e.preg_image)
                .WithOptional(e => e.preg_image_type)
                .HasForeignKey(e => e.image_type_id);

            modelBuilder.Entity<preg_like_type>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<preg_like_type>()
                .HasMany(e => e.preg_daily_like)
                .WithOptional(e => e.preg_like_type)
                .HasForeignKey(e => e.like_type_id);

            modelBuilder.Entity<preg_my_belly>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<preg_my_belly_type>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<preg_my_belly_type>()
                .HasMany(e => e.preg_my_belly)
                .WithOptional(e => e.preg_my_belly_type)
                .HasForeignKey(e => e.my_belly_type_id);

            modelBuilder.Entity<preg_my_birth_plan>()
                .Property(e => e.icon)
                .IsUnicode(false);

            modelBuilder.Entity<preg_my_birth_plan>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_my_birth_plan_type>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<preg_my_birth_plan_type>()
                .HasMany(e => e.preg_my_birth_plan)
                .WithOptional(e => e.preg_my_birth_plan_type)
                .HasForeignKey(e => e.my_birth_plan_type_id);

            modelBuilder.Entity<preg_my_weight_type>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<preg_my_weight_type>()
                .HasMany(e => e.preg_appointment)
                .WithOptional(e => e.preg_my_weight_type)
                .HasForeignKey(e => e.my_weight_type_id);

            modelBuilder.Entity<preg_my_weight_type>()
                .HasMany(e => e.preg_my_weight)
                .WithOptional(e => e.preg_my_weight_type)
                .HasForeignKey(e => e.my_weight_type_id);

            modelBuilder.Entity<preg_page>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<preg_page>()
                .HasMany(e => e.preg_guides)
                .WithOptional(e => e.preg_page)
                .HasForeignKey(e => e.page_id);

            modelBuilder.Entity<preg_phone>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<preg_profession>()
                .Property(e => e.profession_type)
                .IsUnicode(false);

            modelBuilder.Entity<preg_profession>()
                .HasMany(e => e.preg_appointment)
                .WithOptional(e => e.preg_profession)
                .HasForeignKey(e => e.profession_id);

            modelBuilder.Entity<preg_profession>()
                .HasMany(e => e.preg_phone)
                .WithOptional(e => e.preg_profession)
                .HasForeignKey(e => e.profession_id);

            modelBuilder.Entity<preg_question>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_question>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<preg_question>()
                .HasMany(e => e.preg_answer)
                .WithOptional(e => e.preg_question)
                .HasForeignKey(e => e.question_id);

            modelBuilder.Entity<preg_size_guide>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<preg_size_guide>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_size_guide>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<preg_time_frame>()
                .Property(e => e.frame_title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_time_frame>()
                .HasMany(e => e.preg_time_line)
                .WithOptional(e => e.preg_time_frame)
                .HasForeignKey(e => e.time_line_id);

            modelBuilder.Entity<preg_time_line>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_time_line>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<preg_time_line>()
                .Property(e => e.position)
                .IsUnicode(false);

            modelBuilder.Entity<preg_todo>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_todo_other>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_todo_other>()
                .Property(e => e.content)
                .IsUnicode(false);

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
                .Property(e => e.social_type)
                .IsUnicode(false);

            modelBuilder.Entity<preg_user>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<preg_user>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<preg_user>()
                .Property(e => e.you_are_the)
                .IsUnicode(false);

            modelBuilder.Entity<preg_user>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<preg_user>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<preg_user>()
                .Property(e => e.avarta)
                .IsUnicode(false);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_answer)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_appointment)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_auth)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_baby_name)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_cotact_us)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_daily_like)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_my_belly)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_my_birth_plan)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

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
                .HasMany(e => e.preg_question)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_setting)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_todo)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_todo_other)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_upgrade)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_user>()
                .HasMany(e => e.preg_weekly_note)
                .WithOptional(e => e.preg_user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<preg_week>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<preg_week>()
                .Property(e => e.short_description)
                .IsUnicode(false);

            modelBuilder.Entity<preg_week>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<preg_week>()
                .Property(e => e.daily_relation)
                .IsUnicode(false);

            modelBuilder.Entity<preg_week>()
                .HasMany(e => e.preg_image)
                .WithOptional(e => e.preg_week)
                .HasForeignKey(e => e.week_id);

            modelBuilder.Entity<preg_week>()
                .HasMany(e => e.preg_time_line)
                .WithOptional(e => e.preg_week)
                .HasForeignKey(e => e.week_id);

            modelBuilder.Entity<preg_week>()
                .HasMany(e => e.preg_todo)
                .WithOptional(e => e.preg_week)
                .HasForeignKey(e => e.week_id);

            modelBuilder.Entity<preg_week>()
                .HasMany(e => e.preg_weekly_note)
                .WithOptional(e => e.preg_week)
                .HasForeignKey(e => e.week_id);

            modelBuilder.Entity<preg_weekly_note>()
                .Property(e => e.photo)
                .IsUnicode(false);

            modelBuilder.Entity<preg_weekly_note>()
                .Property(e => e.note)
                .IsUnicode(false);
        }
    }
}
