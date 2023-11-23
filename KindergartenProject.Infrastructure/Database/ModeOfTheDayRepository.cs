using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class ModeOfTheDayRepository
    {
        public List<ModeOfTheDayViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.ModesOfTheDays.ToList();
                return ModeOfTheDayMapper.Map(items);
            }
        }

        public ModeOfTheDayViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.ModesOfTheDays.FirstOrDefault(x => x.ID == id);
                return ModeOfTheDayMapper.Map(item);
            }
        }
    }
}
