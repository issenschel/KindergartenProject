using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class PostRepository : IBaseRepository<PostViewModel>
    {
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<PostViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Posts.ToList();
                return PostMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<PostViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Posts.ToList()
                    .Where(x => x.Name.ToLower().Contains(search))
                    .ToList();

                return PostMapper.Map(result);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public PostViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Posts.FirstOrDefault(x => x.ID == id);
                return PostMapper.Map(item);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public PostViewModel Add(PostViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = PostMapper.Map(viewModel);

                context.Posts.Add(entity);
                context.SaveChanges();

                return PostMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public PostViewModel Update(PostViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Posts.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;
                entity.Salary = viewModel.Salary;
                entity.WorkSchedule = viewModel.WorkSchedule;

                context.SaveChanges();

                return PostMapper.Map(entity);
            }
        }

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public PostViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Posts.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                PostViewModel viewModel = PostMapper.Map(entity);

                context.Posts.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
