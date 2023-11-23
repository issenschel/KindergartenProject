using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class MealScheduleRepository
    {
        public List<MealScheduleViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.MealsSchedules.ToList();
                return MealScheduleMapper.Map(items);
            }
        }

        public MealScheduleViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.MealsSchedules.FirstOrDefault(x => x.ID == id);
                return MealScheduleMapper.Map(item);
            }
        }
    }
}
