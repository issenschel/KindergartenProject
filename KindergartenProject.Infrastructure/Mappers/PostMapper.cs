using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Mappers
{
    public static class PostMapper
    {
        public static PostViewModel Map(PostEntity entity)
        {
            var viewModel = new PostViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Salary = entity.Salary,
                WorkSchedule = entity.WorkSchedule
            };
            return viewModel;
        }

        public static List<PostViewModel> Map(List<PostEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
