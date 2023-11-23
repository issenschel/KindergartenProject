using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class GroupRepository
    {
        public List<GroupViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Groups.ToList();
                return GroupMapper.Map(items);
            }
        }

        public GroupViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Groups.FirstOrDefault(x => x.ID == id);
                return GroupMapper.Map(item);
            }
        }
    }
}
