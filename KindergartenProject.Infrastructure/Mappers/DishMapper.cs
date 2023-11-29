using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class DishMapper
    {

        //Преобразует объект класса Entity в объект класса ViewModel
        public static DishViewModel Map(DishEntity entity)
        {
            var viewModel = new DishViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<DishViewModel> Map(List<DishEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
        public static DishEntity Map(DishViewModel viewModel)
        {
            var entity = new DishEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name
            };
            return entity;
        }

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<DishEntity> Map(List<DishViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
