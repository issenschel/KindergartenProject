using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class ModeOfTheDayMapper
    {
        public static ModeOfTheDayViewModel Map(ModeOfTheDayEntity entity)
        {
            var viewModel = new ModeOfTheDayViewModel
            {
                ID = entity.ID,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                OccupationId = entity.OccupationId,
                OccupationName = entity.Occupation.Name,
                GroupId = entity.GroupId,
                GroupName = entity.Group.Name
            };
            return viewModel;
        }

        public static List<ModeOfTheDayViewModel> Map(List<ModeOfTheDayEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }

        public static ModeOfTheDayEntity Map(ModeOfTheDayViewModel viewModel)
        {
            var entity = new ModeOfTheDayEntity
            {
                ID = viewModel.ID,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                OccupationId =viewModel.OccupationId,
                GroupId=viewModel.GroupId
            };
            return entity;
        }

        public static List<ModeOfTheDayEntity> Map(List<ModeOfTheDayViewModel> viewModels)
        {
            var entities = viewModels.Select(vm => Map(vm)).ToList();
            return entities;
        }
    }
}
