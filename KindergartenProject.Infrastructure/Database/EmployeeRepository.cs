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
        public List<EmployeeViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Employees.ToList();
                return EmployeeMapper.Map(items);
            }
        }

        public EmployeeViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Employees.FirstOrDefault(x => x.ID == id);
                return EmployeeMapper.Map(item);
            }
        }

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

        public long? GetLoginIdByName(string loginName)
        {
            using (var context = new Context())
            {
                var login = context.Users.FirstOrDefault(g => g.Login == loginName);
                return login?.ID;
            }
        }

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
