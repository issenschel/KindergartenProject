using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class UserRepository : IBaseRepository<UserViewModel>
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

        public UserViewModel Add(UserViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = UserMapper.Map(viewModel);

                context.Users.Add(entity);
                context.SaveChanges();

                return UserMapper.Map(entity);
            }
        }

        public UserViewModel Update(UserViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Users.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Login = viewModel.Login;
                entity.Password = viewModel.Password;
                entity.RoleId = viewModel.RoleId;

                context.SaveChanges();

                return UserMapper.Map(entity);
            }
        }

        public UserViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Users.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                UserViewModel viewModel = UserMapper.Map(entity);

                context.Users.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
