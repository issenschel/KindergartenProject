using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class EmployeeRepository : IBaseRepository<EmployeeViewModel>
    {
        private DateTime date;

        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<EmployeeViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Employees.ToList();
                return EmployeeMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<EmployeeViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Employees.ToList()
                    .Where(x => x.Post.Name.ToLower().Contains(search) ||
                                x.Name.ToLower().Contains(search) ||
                                x.Surname.ToLower().Contains(search) ||
                                x.Patronymic.ToLower().Contains(search) ||
                                x.Experience.ToString().Contains(search))
                    .ToList();

                return EmployeeMapper.Map(result);
            }
        }
        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public EmployeeViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Employees.FirstOrDefault(x => x.ID == id);
                return EmployeeMapper.Map(item);
            }
        }
        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public EmployeeViewModel Add(EmployeeViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = EmployeeMapper.Map(viewModel);
                if (entity.Name == "Имя")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                if (entity.Surname == "Фамилия")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                if (entity.Patronymic == "Отчество")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                
                if (!DateTime.TryParseExact(entity.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    throw new Exception("Дата должна быть в формате даты (например, 25.11.2023)");
                }
                context.Employees.Add(entity);
                context.SaveChanges();

                return EmployeeMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public EmployeeViewModel Update(EmployeeViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Employees.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;
                if (entity.Name == "Имя")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                entity.Surname = viewModel.Surname;
                if (entity.Surname == "Фамилия")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                entity.Patronymic = viewModel.Patronymic;
                if (entity.Patronymic == "Отчество")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                entity.DateOfBirth = viewModel.DateOfBirth;

                if (!DateTime.TryParseExact(entity.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    throw new Exception("Дата должна быть в формате даты (например, 25.11.2023)");
                }
                entity.Experience = viewModel.Experience;

                entity.UserId = viewModel.UserId;
                entity.PostId = viewModel.PostId;

                context.SaveChanges();

                return EmployeeMapper.Map(entity);
            }
        }

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public EmployeeViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Employees.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                EmployeeViewModel viewModel = EmployeeMapper.Map(entity);

                context.Employees.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
        //Используется для получения идентификатора пользователя по его логину.
        public long? GetLoginIdByName(string loginName)
        {
            using (var context = new Context())
            {
                var login = context.Users.FirstOrDefault(g => g.Login == loginName);
                return login?.ID;
            }
        }
        //Используется для получения списка постов.
        public List<PostViewModel> GetPosts()
        {
            using (var context = new Context())
            {
                var items = context.Posts.ToList();
                return PostMapper.Map(items);
            }
        }

    }
}
