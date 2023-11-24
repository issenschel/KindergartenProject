using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel Map(UserEntity entity)
        {
            var viewModel = new UserViewModel
            {
                ID = entity.ID,
                Login = entity.Login,
                Password = entity.Password,
                RoleId = entity.RoleId,
                RoleName = entity.Role.ID
            };
            return viewModel;
        }

        public static List<UserViewModel> Map(List<UserEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
