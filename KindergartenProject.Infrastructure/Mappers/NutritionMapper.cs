using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class NutritionMapper
    {
        public static NutritionViewModel Map(NutritionEntity entity)
        {
            var viewModel = new NutritionViewModel
            {
                ID = entity.ID,
                BreakFast = entity.BreakFast,
                BreakFastName = entity.Dish.Name,
                Brunch = entity.Brunch,
                BrunchName = entity.Dish1.Name,
                Lunch = entity.Lunch,
                LunchName = entity.Dish2.Name,
                AfternoonSnack = entity.AfternoonSnack,
                AfternoonSnackName = entity.Dish3.Name,
                Dinner = entity.Dinner,
                DinnerName = entity.Dish4.Name
            };
            return viewModel;
        }

        public static List<NutritionViewModel> Map(List<NutritionEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        public static NutritionEntity Map(NutritionViewModel viewModel)
        {
            var entity = new NutritionEntity
            {
                ID = viewModel.ID,
                BreakFast = viewModel.BreakFast,
                Brunch = viewModel.Brunch,
                Lunch = viewModel.Lunch,
                AfternoonSnack = viewModel.AfternoonSnack,
                Dinner = viewModel.Dinner
            };
            return entity;
        }

        public static List<NutritionEntity> Map(List<NutritionViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
