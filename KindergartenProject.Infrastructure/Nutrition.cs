namespace KindergartenProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("nutrition")]
    public partial class NutritionEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NutritionEntity()
        {
            MealSchedule = new HashSet<MealScheduleEntity>();
        }

        public long ID { get; set; }

        public long BreakFast { get; set; }

        public long Brunch { get; set; }

        public long Lunch { get; set; }

        public long AfternoonSnack { get; set; }

        public long Dinner { get; set; }

        public virtual DishEntity Dish { get; set; }

        public virtual DishEntity Dish1 { get; set; }

        public virtual DishEntity Dish2 { get; set; }

        public virtual DishEntity Dish3 { get; set; }

        public virtual DishEntity Dish4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MealScheduleEntity> MealSchedule { get; set; }
    }
}
