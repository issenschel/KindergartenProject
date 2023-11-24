using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class DishRepository
    {
        public List<DishViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Dishes.ToList();
                return DishMapper.Map(items);
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
    }
}
