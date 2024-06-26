﻿using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class KindergartenMapper
    {
        //Преобразует объект класса Entity в объект класса ViewModel
        public static KindergartenViewModel Map(KindergartenEntity entity)
        {
            var viewModel = new KindergartenViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Address = entity.Address,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime

            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<KindergartenViewModel> Map(List<KindergartenEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
        public static KindergartenEntity Map(KindergartenViewModel viewModel)
        {
            var entity = new KindergartenEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
                Address = viewModel.Address,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime
            };
            return entity;
        }

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<KindergartenEntity> Map(List<KindergartenViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
