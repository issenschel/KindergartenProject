using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class OccupationMapper
    {
        public static OccupationViewModel Map(OccupationEntity entity)
        {
            var viewModel = new OccupationViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };
            return viewModel;
        }

        public static List<OccupationViewModel> Map(List<OccupationEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        public static OccupationEntity Map(OccupationViewModel viewModel)
        {
            var entity = new OccupationEntity
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
            };
            return entity;
        }

        public static List<OccupationEntity> Map(List<OccupationViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
