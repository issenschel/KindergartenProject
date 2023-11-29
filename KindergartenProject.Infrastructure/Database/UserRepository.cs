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
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<UserViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Users.ToList();
                return UserMapper.Map(items);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public UserViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Users.FirstOrDefault(x => x.ID == id);
                return UserMapper.Map(item);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
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

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
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

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
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

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
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

        //Отвечает за выполнение входа пользователя.
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
