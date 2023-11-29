using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class RoleMapper
    {
        //Преобразует объект класса Entity в объект класса ViewModel
        public static RoleViewModel Map(RoleEntity entity)
        {
            var viewModel = new RoleViewModel
            {
                ID = entity.ID,
                Name = entity.Name
            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<RoleViewModel> Map(List<RoleEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
        public static RoleEntity Map(RoleViewModel viewModel)
        {
            var entity = new RoleEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name
            };
            return entity;
        }

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<RoleEntity> Map(List<RoleViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
