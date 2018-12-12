namespace _01.Pregnacy_API.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Pregnancy : DbContext
    {
        public Pregnancy()
            : base("name=Pregnancy")
        {
        }

        public virtual DbSet<preg_user> preg_user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<preg_user>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<preg_user>()
                .Property(e => e.phone)
                .IsFixedLength();

            modelBuilder.Entity<preg_user>()
                .Property(e => e.social_type)
                .IsFixedLength();

            modelBuilder.Entity<preg_user>()
                .Property(e => e.first_name)
                .IsFixedLength();

            modelBuilder.Entity<preg_user>()
                .Property(e => e.last_name)
                .IsFixedLength();

            modelBuilder.Entity<preg_user>()
                .Property(e => e.you_are_the)
                .IsFixedLength();

            modelBuilder.Entity<preg_user>()
                .Property(e => e.location)
                .IsFixedLength();

            modelBuilder.Entity<preg_user>()
                .Property(e => e.status)
                .IsFixedLength();

            modelBuilder.Entity<preg_user>()
                .Property(e => e.avarta)
                .IsFixedLength();
        }
    }
}
