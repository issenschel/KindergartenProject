using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class DayOfTheWeekRepository
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
    }
}
