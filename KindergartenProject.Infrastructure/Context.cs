using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KindergartenProject.Infrastructure
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<DayOfTheWeekEntity> DaysOfTheWeeks { get; set; }
        public virtual DbSet<DishEntity> Dishes { get; set; }
        public virtual DbSet<EmployeeEntity> Employees { get; set; }
        public virtual DbSet<GroupEntity> Groups { get; set; }
        public virtual DbSet<KidEntity> Kids { get; set; }
        public virtual DbSet<KindergartenEntity> Kindergartens { get; set; }
        public virtual DbSet<MealScheduleEntity> MealsSchedules { get; set; }
        public virtual DbSet<ModeOfTheDayEntity> ModesOfTheDays { get; set; }
        public virtual DbSet<NutritionEntity> Nutritions { get; set; }
        public virtual DbSet<OccupationEntity> Occupations { get; set; }
        public virtual DbSet<PostEntity> Posts { get; set; }
        public virtual DbSet<RoleEntity> Roles { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DayOfTheWeekEntity>()
                .HasMany(e => e.MealSchedule)
                .WithRequired(e => e.DayOfTheWeek)
                .HasForeignKey(e => e.DayOfTheWeekId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DishEntity>()
                .HasMany(e => e.Nutrition)
                .WithRequired(e => e.Dish)
                .HasForeignKey(e => e.BreakFast)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DishEntity>()
                .HasMany(e => e.Nutrition1)
                .WithRequired(e => e.Dish1)
                .HasForeignKey(e => e.Dinner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DishEntity>()
                .HasMany(e => e.Nutrition2)
                .WithRequired(e => e.Dish2)
                .HasForeignKey(e => e.Brunch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DishEntity>()
                .HasMany(e => e.Nutrition3)
                .WithRequired(e => e.Dish3)
                .HasForeignKey(e => e.AfternoonSnack)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DishEntity>()
                .HasMany(e => e.Nutrition4)
                .WithRequired(e => e.Dish4)
                .HasForeignKey(e => e.Lunch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeEntity>()
                .HasMany(e => e.Group)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupEntity>()
                .HasMany(e => e.Kid)
                .WithRequired(e => e.Group)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupEntity>()
                .HasMany(e => e.ModeOfTheDay)
                .WithRequired(e => e.Group)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NutritionEntity>()
                .HasMany(e => e.MealSchedule)
                .WithRequired(e => e.Nutrition)
                .HasForeignKey(e => e.NutritionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OccupationEntity>()
                .HasMany(e => e.ModeOfTheDay)
                .WithRequired(e => e.Occupation)
                .HasForeignKey(e => e.OccupationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PostEntity>()
                .Property(e => e.Salary)
                .HasPrecision(53, 0);

            modelBuilder.Entity<PostEntity>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleEntity>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
