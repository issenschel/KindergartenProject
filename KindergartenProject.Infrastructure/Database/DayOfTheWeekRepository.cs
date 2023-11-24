using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class DayOfTheWeekRepository : IBaseRepository<DayOfTheWeekViewModel>
    {
        public List<DayOfTheWeekViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.DaysOfTheWeeks.ToList();
                return DayOfTheWeekMapper.Map(items);
            }
        }

        public DayOfTheWeekViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.DaysOfTheWeeks.FirstOrDefault(x => x.ID == id);
                return DayOfTheWeekMapper.Map(item);
            }
        }

        public DayOfTheWeekViewModel Add(DayOfTheWeekViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = DayOfTheWeekMapper.Map(viewModel);

                context.DaysOfTheWeeks.Add(entity);
                context.SaveChanges();

                return DayOfTheWeekMapper.Map(entity);
            }
        }

        public DayOfTheWeekViewModel Update(DayOfTheWeekViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.DaysOfTheWeeks.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;

                context.SaveChanges();

                return DayOfTheWeekMapper.Map(entity);
            }
        }

        public DayOfTheWeekViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.DaysOfTheWeeks.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                DayOfTheWeekViewModel viewModel = DayOfTheWeekMapper.Map(entity);

                context.DaysOfTheWeeks.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
