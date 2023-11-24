using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class ModeOfTheDayRepository : IBaseRepository<ModeOfTheDayViewModel>
    {
        public List<ModeOfTheDayViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.ModesOfTheDays.ToList();
                return ModeOfTheDayMapper.Map(items);
            }
        }

        public ModeOfTheDayViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.ModesOfTheDays.FirstOrDefault(x => x.ID == id);
                return ModeOfTheDayMapper.Map(item);
            }
        }

        public List<ModeOfTheDayViewModel> GetByGroupId(long groupId)
        {
            using (var context = new Context())
            {
                var items = context.ModesOfTheDays.Where(m => m.GroupId == groupId).ToList();
                return ModeOfTheDayMapper.Map(items);
            }
        }

        public ModeOfTheDayViewModel Add(ModeOfTheDayViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = ModeOfTheDayMapper.Map(viewModel);

                context.ModesOfTheDays.Add(entity);
                context.SaveChanges();

                return ModeOfTheDayMapper.Map(entity);
            }
        }

        public ModeOfTheDayViewModel Update(ModeOfTheDayViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.ModesOfTheDays.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.StartTime = viewModel.StartTime;
                entity.EndTime = viewModel.EndTime;
                entity.OccupationId = viewModel.OccupationId;
                entity.GroupId = viewModel.GroupId;

                context.SaveChanges();

                return ModeOfTheDayMapper.Map(entity);
            }
        }

        public ModeOfTheDayViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.ModesOfTheDays.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                ModeOfTheDayViewModel viewModel = ModeOfTheDayMapper.Map(entity);

                context.ModesOfTheDays.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
