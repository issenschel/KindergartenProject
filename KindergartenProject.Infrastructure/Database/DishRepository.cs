using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class DishRepository : IBaseRepository<DishViewModel>
    {
        public List<DishViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Dishes.ToList();
                return DishMapper.Map(items);
            }
        }

        public List<DishViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Dishes
                    .Where(x => x.Name.ToLower().Contains(search)).ToList();

                return DishMapper.Map(result);
            }
        }

        public DishViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Dishes.FirstOrDefault(x => x.ID == id);
                return DishMapper.Map(item);
            }
        }

        public DishViewModel Add(DishViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = DishMapper.Map(viewModel);

                context.Dishes.Add(entity);
                context.SaveChanges();

                return DishMapper.Map(entity);
            }
        }

        public DishViewModel Update(DishViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Dishes.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;

                context.SaveChanges();

                return DishMapper.Map(entity);
            }
        }

        public DishViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Dishes.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                DishViewModel viewModel = DishMapper.Map(entity);

                context.Dishes.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
