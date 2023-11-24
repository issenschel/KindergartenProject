using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class DayOfTheWeekMapper
    {
        public static DayOfTheWeekViewModel Map(DayOfTheWeekEntity entity)
        {
            var viewModel = new DayOfTheWeekViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };
            return viewModel;
        }

        public static List<DayOfTheWeekViewModel> Map(List<DayOfTheWeekEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        public static DayOfTheWeekEntity Map(DayOfTheWeekViewModel viewModel)
        {
            var entity = new DayOfTheWeekEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name
            };
            return entity;
        }

        public static List<DayOfTheWeekEntity> Map(List<DayOfTheWeekViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
