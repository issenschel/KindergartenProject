using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class KindergartenMapper
    {
        public static KindergartenViewModel Map(KindergartenEntity entity)
        {
            var viewModel = new KindergartenViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Address = entity.Address,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime

            };
            return viewModel;
        }

        public static List<KindergartenViewModel> Map(List<KindergartenEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
