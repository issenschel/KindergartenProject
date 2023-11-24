using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class UserRepository
    {
        public List<UserViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Users.ToList();
                return UserMapper.Map(items);
            }
        }

        public UserViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Users.FirstOrDefault(x => x.ID == id);
                return UserMapper.Map(item);
            }
        }
    }
}
