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
                var entity = NutritionMapper.Map(viewModel);

                context.Nutritions.Add(entity);
                context.SaveChanges();

                return NutritionMapper.Map(entity);
            }
        }

        public NutritionViewModel Update(NutritionViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Nutritions.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.BreakFast = viewModel.BreakFast;
                entity.Lunch = viewModel.Lunch;
                entity.AfternoonSnack = viewModel.AfternoonSnack;
                entity.Dinner = viewModel.Dinner;
                entity.Brunch = viewModel.Brunch;

                context.SaveChanges();

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
