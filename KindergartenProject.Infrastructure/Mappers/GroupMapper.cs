using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class GroupMapper
    {
        public static GroupViewModel Map(GroupEntity entity)
        {
            var viewModel = new GroupViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                AvailableSeats = entity.AvailableSeats,
                EmployeeId = entity.EmployeeId,
                EmployeeName = $"{entity.Employee?.Surname} {entity.Employee?.Name} {entity.Employee?.Patronymic}"
            };
            return viewModel;
        }

        public static List<GroupViewModel> Map(List<GroupEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        public static GroupEntity Map(GroupViewModel viewModel)
        {
            var entity = new GroupEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
                AvailableSeats = viewModel.AvailableSeats,
                EmployeeId = viewModel.EmployeeId
            };
            return entity;
        }

        public static List<GroupEntity> Map(List<GroupViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
