namespace KindergartenProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mealSchedule")]
    public partial class MealScheduleEntity
    {
        public long ID { get; set; }

        [Column("DayOfTheWeek_ID")]
        public long DayOfTheWeekId { get; set; }

        [Column("Nutrition_ID")]
        public long NutritionId { get; set; }

        public virtual DayOfTheWeekEntity DayOfTheWeek { get; set; }

        public virtual NutritionEntity Nutrition { get; set; }
    }
}
