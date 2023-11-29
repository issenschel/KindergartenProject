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
        //Преобразует объект класса Entity в объект класса ViewModel
        public static NutritionViewModel Map(NutritionEntity entity)
        {
            var viewModel = new NutritionViewModel
            {
                ID = entity.ID,
                BreakFast = entity.BreakFast,
                BreakFastName = entity.Dish.Name,
                Brunch = entity.Brunch,
                BrunchName = entity.Dish2.Name,
                Lunch = entity.Lunch,
                LunchName = entity.Dish4.Name,
                AfternoonSnack = entity.AfternoonSnack,
                AfternoonSnackName = entity.Dish3.Name,
                Dinner = entity.Dinner,
                DinnerName = entity.Dish1.Name
            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<NutritionViewModel> Map(List<NutritionEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
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

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<NutritionEntity> Map(List<NutritionViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
