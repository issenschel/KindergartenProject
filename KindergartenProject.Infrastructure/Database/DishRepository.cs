using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class DishRepository : IBaseRepository<DishViewModel>
    {
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<DishViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Dishes.ToList();
                return DishMapper.Map(items);
            }
        }


        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<DishViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Dishes
                    .Where(x => x.Name.ToLower().Contains(search)).ToList();

                return DishMapper.Map(result);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public DishViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Dishes.FirstOrDefault(x => x.ID == id);
                return DishMapper.Map(item);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public DishViewModel Add(DishViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = DishMapper.Map(viewModel);

                context.Dishes.Add(entity);
                context.SaveChanges();

                return DishMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public DishViewModel Update(DishViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Dishes.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;

                context.SaveChanges();

                return DishMapper.Map(entity);
            }
        }
        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public DishViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Dishes.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                DishViewModel viewModel = DishMapper.Map(entity);

                context.Dishes.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
