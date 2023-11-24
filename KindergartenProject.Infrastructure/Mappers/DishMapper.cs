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
        public static DishViewModel Map(DishEntity entity)
        {
            var viewModel = new DishViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };
            return viewModel;
        }

        public static List<DishViewModel> Map(List<DishEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
