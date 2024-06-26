﻿using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class OccupationMapper
    {
        //Преобразует объект класса Entity в объект класса ViewModel
        public static OccupationViewModel Map(OccupationEntity entity)
        {
            var viewModel = new OccupationViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<OccupationViewModel> Map(List<OccupationEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
        public static OccupationEntity Map(OccupationViewModel viewModel)
        {
            var entity = new OccupationEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
            };
            return entity;
        }

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<OccupationEntity> Map(List<OccupationViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
