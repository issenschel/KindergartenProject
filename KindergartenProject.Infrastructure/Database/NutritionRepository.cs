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
                viewModel.BreakFast = GetOrCreateOrUpdateDishId(context, viewModel.BreakFastName);
                viewModel.Brunch = GetOrCreateOrUpdateDishId(context, viewModel.BrunchName);
                viewModel.Lunch = GetOrCreateOrUpdateDishId(context, viewModel.LunchName);
                viewModel.AfternoonSnack = GetOrCreateOrUpdateDishId(context, viewModel.AfternoonSnackName);
                viewModel.Dinner = GetOrCreateOrUpdateDishId(context, viewModel.DinnerName);

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
                // Если блюдо с указанным ID существует и имя изменилось, обновляем его имя и сохраняем изменения
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
                entity.Brunch = GetOrCreateOrUpdateDishId(context, viewModel.BrunchName, entity.Brunch);
                entity.Lunch = GetOrCreateOrUpdateDishId(context, viewModel.LunchName, entity.Lunch);
                entity.AfternoonSnack = GetOrCreateOrUpdateDishId(context, viewModel.AfternoonSnackName, entity.AfternoonSnack);
                entity.Dinner = GetOrCreateOrUpdateDishId(context, viewModel.DinnerName, entity.Dinner);

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
