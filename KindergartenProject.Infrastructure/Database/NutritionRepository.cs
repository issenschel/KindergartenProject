using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class NutritionRepository
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
    }
}
