using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeViewModel Map(EmployeeEntity entity)
        {
            var viewModel = new EmployeeViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Surname = entity.Surname,
                Patronymic = entity.Patronymic,
                FullName = $"{entity.Surname} {entity.Name} {entity.Patronymic}",
                DateOfBirth = entity.DateOfBirth,
                Experience = entity.Experience,
                PostId = entity.PostId,
                PostName = entity.Post?.Name,
                UserId = entity.UserId,
                UserName = entity.User?.Login
            };
            return viewModel;
        }

        public static List<EmployeeViewModel> Map(List<EmployeeEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        public static EmployeeEntity Map(EmployeeViewModel viewModel)
        {
            var entity = new EmployeeEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
                Surname = viewModel.Surname,
                Patronymic = viewModel.Patronymic,
                Experience = viewModel.Experience,
                DateOfBirth = viewModel.DateOfBirth,
                PostId = viewModel.PostId,
                UserId = viewModel.UserId
            };
            return entity;
        }

        public static List<EmployeeEntity> Map(List<EmployeeViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
