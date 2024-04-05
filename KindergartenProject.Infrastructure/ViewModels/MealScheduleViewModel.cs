using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class MealScheduleViewModel
    {
        public long ID { get; set; }

        public long DayOfTheWeekId { get; set; }

        public string DayOfTheWeekName { get; set; }

        public long NutritionId { get; set; }

    }
}
