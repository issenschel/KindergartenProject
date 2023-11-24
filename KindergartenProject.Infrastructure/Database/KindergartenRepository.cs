using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class KindergartenRepository
    {
        public List<KindergartenViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Kindergartens.ToList();
                return KindergartenMapper.Map(items);
            }
        }

        public KindergartenViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Kindergartens.FirstOrDefault(x => x.ID == id);
                return KindergartenMapper.Map(item);
            }
        }
    }
}
