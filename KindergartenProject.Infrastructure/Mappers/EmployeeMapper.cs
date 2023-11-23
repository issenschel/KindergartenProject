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
                FullName = $"{entity.Name} {entity.Surname} {entity.Patronymic}",
                DateOfBirth = entity.DateOfBirth,
                PostId = entity.PostId,
                PostName = entity.Post.Name,
                UserId = entity.UserId,
                UserName = entity.User.Login
            };
            return viewModel;
        }

        public static List<EmployeeViewModel> Map(List<EmployeeEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
