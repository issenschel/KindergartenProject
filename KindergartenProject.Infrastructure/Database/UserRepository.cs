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

        public List<UserViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Users.ToList()
                    .Where(x => x.Login.ToLower().Contains(search))
                    .ToList();

                return UserMapper.Map(result);
            }
        }

        public UserViewModel Add(UserViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = UserMapper.Map(viewModel);

                context.Users.Add(entity);
                if (string.IsNullOrEmpty(entity.Login))
                    throw new Exception("Имя пользователя не может быть пустым");
                if (string.IsNullOrEmpty(entity.Password))
                    throw new Exception("Пароль не может быть пустым");
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

                entity.Login = viewModel.Login.Trim();
                if (string.IsNullOrEmpty(entity.Login))
                    throw new Exception("Имя пользователя не может быть пустым");
                entity.Password = viewModel.Password;
                if (string.IsNullOrEmpty(entity.Password))
                    throw new Exception("Пароль не может быть пустым");
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


        public UserViewModel Login(string login, string password)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);

                if (user != null)
                {
                    return UserMapper.Map(user);
                }

                return null;
            }
        }
    }
}
