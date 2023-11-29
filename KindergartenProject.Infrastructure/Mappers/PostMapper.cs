using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class PostMapper
    {
        //Преобразует объект класса Entity в объект класса ViewModel
        public static PostViewModel Map(PostEntity entity)
        {
            var viewModel = new PostViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Salary = entity.Salary,
                WorkSchedule = entity.WorkSchedule
            };
            return viewModel;
        }

        //Принимает список объектов класса Entity и преобразует каждый объект в соответствующий объект класса
        public static List<PostViewModel> Map(List<PostEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        //Метод Map преобразует объект класса ViewModel в объект класса Entity
        public static PostEntity Map(PostViewModel viewModel)
        {
            var entity = new PostEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
                Salary = viewModel.Salary,
                WorkSchedule = viewModel.WorkSchedule
            };
            return entity;
        }

        //Принимает список объектов класса ViewModel и преобразует каждый объект в соответствующий объект класса
        public static List<PostEntity> Map(List<PostViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
