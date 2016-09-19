namespace ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<InformationUsers> InformationUsers { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photos>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Photos)
                .Map(m => m.ToTable("TagPhotoes").MapLeftKey("Photo_PhotoId").MapRightKey("Tag_Id"));

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRoles").MapLeftKey("Role_RoleId").MapRightKey("User_Id"));

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Photos)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.User_Id);
        }
    }
}
