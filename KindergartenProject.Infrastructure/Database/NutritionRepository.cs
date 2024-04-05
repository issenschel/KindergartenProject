using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class NutritionRepository : IBaseRepository<NutritionViewModel>
    {
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<NutritionViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Nutritions.ToList();
                return NutritionMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<NutritionViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Nutritions.ToList()
                    .Where(x => x.Dinner.ToString().Contains(search))
                    .ToList();

                return NutritionMapper.Map(result);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public NutritionViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Nutritions.FirstOrDefault(x => x.ID == id);
                return NutritionMapper.Map(item);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public NutritionViewModel Add(NutritionViewModel viewModel)
        {
            using (var context = new Context())
            {
                viewModel.BreakFast = GetOrCreateOrUpdateDishId(context, viewModel.BreakFastName);
                if (viewModel.BreakFastName == "Завтрак")
                {
                    throw new Exception("Завтрак не может быть пустым");
                }

                viewModel.Brunch = GetOrCreateOrUpdateDishId(context, viewModel.BrunchName);

                if (viewModel.BrunchName == "Ланч")
                {
                    throw new Exception("Ланч не может быть пустым");
                }

                viewModel.Lunch = GetOrCreateOrUpdateDishId(context, viewModel.LunchName);

                if (viewModel.LunchName == "Обед")
                {
                    throw new Exception("Обед не может быть пустым");
                }
                viewModel.AfternoonSnack = GetOrCreateOrUpdateDishId(context, viewModel.AfternoonSnackName);

                if (viewModel.AfternoonSnackName == "Полдник")
                {
                    throw new Exception("Полдник не может быть пустым");
                }

                viewModel.Dinner = GetOrCreateOrUpdateDishId(context, viewModel.DinnerName);

                if (viewModel.DinnerName == "Полдник")
                {
                    throw new Exception("Ужин не может быть пустым");
                }



                var existingNutrition = context.Nutritions.FirstOrDefault(n => n.BreakFast == viewModel.BreakFast &&
                                             n.Brunch == viewModel.Brunch &&
                                             n.Lunch == viewModel.Lunch &&
                                             n.AfternoonSnack == viewModel.AfternoonSnack &&
                                             n.Dinner == viewModel.Dinner);

                if (existingNutrition != null)
                {
                    // Если такой план питания уже есть, возвращаем его
                    return NutritionMapper.Map(existingNutrition);
                }
                else
                {
                    // Если нет, создаем новый
                    var entity = NutritionMapper.Map(viewModel);

                    context.Nutritions.Add(entity);
                    context.SaveChanges();

                    return NutritionMapper.Map(entity); // Исправлено здесь
                }
            }
        }

        //Создание или обновление еды
        public long GetOrCreateOrUpdateDishId(Context context, string dishName, long? dishId = null)
        {
            var existingDish = context.Dishes.FirstOrDefault(d => d.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase));

            if (existingDish != null)
            {
                // Если блюдо с указанным именем уже существует, возвращаем его ID
                return existingDish.ID;
            }

            var dish = dishId.HasValue ? context.Dishes.FirstOrDefault(d => d.ID == dishId.Value) : null;

            if (dish != null && !dish.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase))
            {
                dish.Name = dishName;
                context.SaveChanges();
            }
            else if (dish == null)
            {
                // Если блюдо с указанным ID не существует, создаем новое блюдо с указанным именем
                dish = new DishEntity { ID = 0, Name = dishName }; // Указываем значение для поля ID
                context.Dishes.Add(dish);
                context.SaveChanges();
            }

            // Возвращаем ID блюда
            return dish.ID;
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public NutritionViewModel Update(NutritionViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Nutritions.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                {
                    return null;
                }

                // Обновить существующие блюда или создать новые, если имя изменилось
                entity.BreakFast = GetOrCreateOrUpdateDishId(context, viewModel.BreakFastName, entity.BreakFast);
                if (viewModel.BreakFastName == "Завтрак")
                {
                    throw new Exception("Завтрак не может быть пустым");
                }

                entity.Brunch = GetOrCreateOrUpdateDishId(context, viewModel.BrunchName, entity.Brunch);

                if (viewModel.BrunchName == "Ланч")
                {
                    throw new Exception("Ланч не может быть пустым");
                }

                entity.Lunch = GetOrCreateOrUpdateDishId(context, viewModel.LunchName, entity.Lunch);
                if (viewModel.LunchName == "Обед")
                {
                    throw new Exception("Обед не может быть пустым");
                }

                entity.AfternoonSnack = GetOrCreateOrUpdateDishId(context, viewModel.AfternoonSnackName, entity.AfternoonSnack);

                if (viewModel.AfternoonSnackName == "Полдник")
                {
                    throw new Exception("Полдник не может быть пустым");
                }

                entity.Dinner = GetOrCreateOrUpdateDishId(context, viewModel.DinnerName, entity.Dinner);

                if (viewModel.DinnerName == "Ужин")
                {
                    throw new Exception("Ужин не может быть пустым");
                }

                // Сохранить обновления в контексте
                context.SaveChanges();

                // Вернуть обновленную модель viewModel
                return NutritionMapper.Map(entity);
            }
        }

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public NutritionViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Nutritions.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                NutritionViewModel viewModel = NutritionMapper.Map(entity);

                context.Nutritions.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
