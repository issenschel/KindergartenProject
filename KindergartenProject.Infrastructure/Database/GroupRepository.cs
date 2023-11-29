using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class GroupRepository : IBaseRepository<GroupViewModel>
    {

        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<GroupViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Groups.ToList();
                return GroupMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<GroupViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Groups.ToList()
                    .Where(x => x.Name.ToLower().Contains(search))
                    .ToList();

                return GroupMapper.Map(result);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public GroupViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Groups.FirstOrDefault(x => x.ID == id);
                return GroupMapper.Map(item);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public GroupViewModel Add(GroupViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = GroupMapper.Map(viewModel);
                if (entity.Name == "Имя")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                context.Groups.Add(entity);
                context.SaveChanges();

                return GroupMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public GroupViewModel Update(GroupViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Groups.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;
                if (entity.Name == "Имя")
                {
                    throw new Exception("Имя не может быть пустым");
                }
                entity.AvailableSeats = viewModel.AvailableSeats;
                entity.EmployeeId = viewModel.EmployeeId;

                context.SaveChanges();

                return GroupMapper.Map(entity);
            }
        }

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public GroupViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Groups.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                GroupViewModel viewModel = GroupMapper.Map(entity);

                context.Groups.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
        // Используется для получения списка сотрудников.
        public List<EmployeeViewModel> GetEmployees()
        {
            using (var context = new Context())
            {
                var items = context.Employees.ToList();
                return EmployeeMapper.Map(items);
            }
        }
    }
}
