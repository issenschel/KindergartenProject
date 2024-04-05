using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class KidMapper
    {
        //Преобразует объект класса Entity в объект класса ViewModel
        public static KidViewModel Map(KidEntity entity)
        {
            var viewModel = new KidViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Surname = entity.Surname,
                Patronymic = entity.Patronymic,
                FullName = $"{entity.Surname} {entity.Name} {entity.Patronymic}",
                DateOfBirth = entity.DateOfBirth,
                GroupId = entity.GroupId,
                GroupName = entity.Group?.Name,
            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<KidViewModel> Map(List<KidEntity> entities) 
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
        public static KidEntity Map(KidViewModel viewModel)
        {
            var entity = new KidEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
                Surname = viewModel.Surname,
                Patronymic = viewModel.Patronymic,
                DateOfBirth = viewModel.DateOfBirth,
                GroupId = viewModel.GroupId
            };
            return entity;
        }

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<KidEntity> Map(List<KidViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
