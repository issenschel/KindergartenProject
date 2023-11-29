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
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<MealScheduleViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.MealsSchedules.ToList();
                return MealScheduleMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<MealScheduleViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.MealsSchedules.ToList()
                    .Where(x => x.DayOfTheWeek.ToString().Contains(search))
                    .ToList();

                return MealScheduleMapper.Map(result);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public MealScheduleViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.MealsSchedules.FirstOrDefault(x => x.ID == id);
                return MealScheduleMapper.Map(item);
            }
        }
        //Используется для получения списка дней недели.
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

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
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

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
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

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
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
        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
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
