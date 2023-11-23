using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class KidRepository
    {
        public List<KidViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Kids.ToList();
                return KidMapper.Map(items);
            }
        }

        public KidViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Kids.FirstOrDefault(x => x.ID == id);
                return KidMapper.Map(item);
            }
        }


    }
}
