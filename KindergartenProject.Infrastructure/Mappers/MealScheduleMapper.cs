using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class MealScheduleMapper
    {
        public static MealScheduleViewModel Map(MealScheduleEntity entity)
        {
            var viewModel = new MealScheduleViewModel
            {
                ID = entity.ID,
                DayOfTheWeekId = entity.DayOfTheWeekId,
                DayOfTheWeekName = entity.DayOfTheWeek.Name,
                NutritionId = entity.NutritionId,
            };
            return viewModel;
        }

        public static List<MealScheduleViewModel> Map(List<MealScheduleEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

    }
}
