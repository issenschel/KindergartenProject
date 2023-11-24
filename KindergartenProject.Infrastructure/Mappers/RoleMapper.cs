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
        public static RoleViewModel Map(RoleEntity entity)
        {
            var viewModel = new RoleViewModel
            {
                ID = entity.ID,
                Name = entity.Name
            };
            return viewModel;
        }

        public static List<RoleViewModel> Map(List<RoleEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
