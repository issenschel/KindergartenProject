using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class MealScheduleRepository : IBaseRepository<MealScheduleViewModel>
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

        public List<DayOfTheWeekViewModel> GetDaysOfTheWeeks()
        {
            using (var context = new Context())
            {
                var items = context.DaysOfTheWeeks.ToList();
                return DayOfTheWeekMapper.Map(items);
            }
        }

        // Метод для получения расписаний питания по ID дня недели
        public List<MealScheduleViewModel> GetByDayId(long dayId)
        {
            using (var context = new Context())
            {
                var items = context.MealsSchedules.Where(ms => ms.DayOfTheWeekId == dayId).ToList();
                return MealScheduleMapper.Map(items);
            }
        }


        public MealScheduleViewModel Add(MealScheduleViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = MealScheduleMapper.Map(viewModel);

                context.MealsSchedules.Add(entity);
                context.SaveChanges();

                return MealScheduleMapper.Map(entity);
            }
        }


        public MealScheduleViewModel Update(MealScheduleViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.MealsSchedules.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.DayOfTheWeekId = viewModel.DayOfTheWeekId;
                entity.NutritionId = viewModel.NutritionId;

                context.SaveChanges();

                return MealScheduleMapper.Map(entity);
            }
        }

        public MealScheduleViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.MealsSchedules.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                MealScheduleViewModel viewModel = MealScheduleMapper.Map(entity);

                context.MealsSchedules.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }

        public MealScheduleViewModel GetByNutritionId(long nutritionId)
        {
            using (var context = new Context())
            {
                var item = context.MealsSchedules.FirstOrDefault(x => x.NutritionId == nutritionId);
                return MealScheduleMapper.Map(item);
            }
        }

    }
}
