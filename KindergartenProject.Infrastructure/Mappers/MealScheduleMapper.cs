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

        public static MealScheduleEntity Map(MealScheduleViewModel viewModel)
        {
            var entity = new MealScheduleEntity
            {
                ID = viewModel.ID,
                DayOfTheWeekId = viewModel.DayOfTheWeekId,
                NutritionId = viewModel.NutritionId
            };
            return entity;
        }

        public static List<MealScheduleEntity> Map(List<MealScheduleViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }

    }
}
