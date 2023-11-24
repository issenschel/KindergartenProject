using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class OccupationRepository
    {
        public List<OccupationViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Occupations.ToList();
                return OccupationMapper.Map(items);
            }
        }

        public OccupationViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Occupations.FirstOrDefault(x => x.ID == id);
                return OccupationMapper.Map(item);
            }
        }
    }
}
