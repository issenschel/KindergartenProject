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
    public class KidRepository : IBaseRepository<KidViewModel>
    {
        private DateTime date;

        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<KidViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Kids.ToList();
                return KidMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<KidViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Kids.ToList()
                    .Where(x => x.Group.Name.ToLower().Contains(search) ||
                                x.Name.ToLower().Contains(search) ||
                                x.Surname.ToLower().Contains(search) ||
                                x.Patronymic.ToLower().Contains(search) ||
                                x.DateOfBirth.ToLower().Contains(search))
                    .ToList();

                return KidMapper.Map(result);
            }
        }
        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public KidViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Kids.FirstOrDefault(x => x.ID == id);
                return KidMapper.Map(item);
            }
        }
        //используется для получения списка групп.
        public List<GroupViewModel> GetGroups()
        {
            using (var context = new Context())
            {
                var items = context.Groups.ToList();
                return GroupMapper.Map(items);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public KidViewModel Add(KidViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = KidMapper.Map(viewModel);


                if (entity.Name == "Имя")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                if (entity.Surname == "Фамилия")
                {
                    throw new Exception("Фамилия не может быть пустой");
                }

                if (entity.Patronymic == "Отчество")
                {
                    entity.Patronymic = "";
                }

                if (!DateTime.TryParseExact(entity.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    throw new Exception("Дата должна быть в формате даты (например, 25.11.2023)");
                }


                context.Kids.Add(entity);

                context.SaveChanges();

                return KidMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public KidViewModel Update(KidViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Kids.FirstOrDefault(x => x.ID == viewModel.ID);
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
                    throw new Exception("Фамилия не может быть пустой");
                }

                entity.Patronymic = viewModel.Patronymic;
                if (entity.Patronymic == "Отчество")
                {
                    entity.Patronymic = "";
                }
                entity.DateOfBirth = viewModel.DateOfBirth;

                if (!DateTime.TryParseExact(entity.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    throw new Exception("Дата должна быть в формате даты (например, 25.11.2023)");
                }
                entity.GroupId = viewModel.GroupId;

                context.SaveChanges();

                return KidMapper.Map(entity);
            }
        }

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public KidViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Kids.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                KidViewModel viewModel = KidMapper.Map(entity);

                context.Kids.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
