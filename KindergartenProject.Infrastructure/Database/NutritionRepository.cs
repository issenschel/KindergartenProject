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
        public List<NutritionViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Nutritions.ToList();
                return NutritionMapper.Map(items);
            }
        }

        public NutritionViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Nutritions.FirstOrDefault(x => x.ID == id);
                return NutritionMapper.Map(item);
            }
        }

        public NutritionViewModel Add(NutritionViewModel viewModel)
        {
            using (var context = new Context())
            {
                // Получаем или создаем сущности Dish для каждого блюда
                viewModel.BreakFast = GetOrCreateDishId(context, viewModel.BreakFastName);
                viewModel.Brunch = GetOrCreateDishId(context, viewModel.BrunchName);
                viewModel.Lunch = GetOrCreateDishId(context, viewModel.LunchName);
                viewModel.AfternoonSnack = GetOrCreateDishId(context, viewModel.AfternoonSnackName);
                viewModel.Dinner = GetOrCreateDishId(context, viewModel.DinnerName);

                // Проверяем, существует ли уже такое же питание
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

                    return NutritionMapper.Map(entity);
                }
            }
        }

        public long GetOrCreateDishId(Context context, string dishName)
        {
            // Пытаемся найти существующее блюдо по имени
            var dish = context.Dishes.FirstOrDefault(d => d.Name.ToLower() == dishName.ToLower());
            if (dish != null)
            {
                // Если блюдо с таким именем найдено, возвращаем его ID
                return dish.ID;
            }

            // Если блюдо не найдено, создаем новое с указанным именем
            dish = new DishEntity { Name = dishName };
            context.Dishes.Add(dish);
            context.SaveChanges();

            // Возвращаем ID нового блюда
            return dish.ID;
        }
        public long GetOrUpdateDishId(Context context, string dishName, long? dishId = null)
        {
            // Пытаемся найти существующее блюдо по имени
            var dish = dishId.HasValue ? context.Dishes.Find(dishId.Value)
                                       : null;

            if (dish != null && !dish.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase))
            {
                // Проверяем, существует ли уже блюдо с новым именем
                var existingDish = context.Dishes.FirstOrDefault(d => d.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase));

                if (existingDish != null)
                {
                    // Если существует, возвращаем его ID
                    return existingDish.ID;
                }
                else
                {
                    // Если нет, обновляем текущее блюдо
                    dish.Name = dishName;
                    context.SaveChanges();
                    return dish.ID;
                }
            }
            else if (dish == null)
            {
                // Если блюдо с данным ID не найдено или не передан ID, ищем по имени
                dish = context.Dishes.FirstOrDefault(d => d.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase));

                if (dish != null)
                {
                    // Если найдено существующее блюдо с таким именем, возвращаем его ID
                    return dish.ID;
                }
                else
                {
                    // Блюдо с таким именем не найдено, создаем новое
                    dish = new DishEntity { Name = dishName };
                    context.Dishes.Add(dish);
                    context.SaveChanges();
                    return dish.ID;
                }
            }
            else
            {
                // Если блюдо существует и имя не изменено, возвращаем существующий ID
                return dish.ID;
            }
        }
        public NutritionViewModel Update(NutritionViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Nutritions.FirstOrDefault(x => x.ID == viewModel.ID);

                if (entity == null)
                    return null;

                // Обновить существующие блюда или создать новые, если имя изменилось
                entity.BreakFast = GetOrUpdateDishId(context, viewModel.BreakFastName, entity.BreakFast);
                entity.Brunch = GetOrUpdateDishId(context, viewModel.BrunchName, entity.Brunch);
                entity.Lunch = GetOrUpdateDishId(context, viewModel.LunchName, entity.Lunch);
                entity.AfternoonSnack = GetOrUpdateDishId(context, viewModel.AfternoonSnackName, entity.AfternoonSnack);
                entity.Dinner = GetOrUpdateDishId(context, viewModel.DinnerName, entity.Dinner);

                // Сохранить обновления в контексте
                context.SaveChanges();

                // Вернуть обновленную модель viewModel
                return NutritionMapper.Map(entity);
            }
        }

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
