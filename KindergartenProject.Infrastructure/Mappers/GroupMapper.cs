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
                EmployeeName = $"{entity.Employee.Surname} {entity.Employee.Name} {entity.Employee.Patronymic}"
            };
            return viewModel;
        }

        public static List<GroupViewModel> Map(List<GroupEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
