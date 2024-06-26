﻿using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class MealScheduleMapper
    {
        //Преобразует объект класса Entity в объект класса ViewModel
        public static MealScheduleViewModel Map(MealScheduleEntity entity)
        {
            var viewModel = new MealScheduleViewModel
            {
                ID = entity.ID,
                DayOfTheWeekId = entity.DayOfTheWeekId,
                DayOfTheWeekName = entity.DayOfTheWeek?.Name,
                NutritionId = entity.NutritionId,
            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<MealScheduleViewModel> Map(List<MealScheduleEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
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

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<MealScheduleEntity> Map(List<MealScheduleViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }

    }
}
