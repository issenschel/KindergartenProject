using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class OccupationRepository : IBaseRepository<OccupationViewModel>
    {
        //Получает все объекты из базы данных и возвращает их список после применения маппинга
        public List<OccupationViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Occupations.ToList();
                return OccupationMapper.Map(items);
            }
        }

        //Выполняет поиск объектов по указанной строке.Возвращает список найденных объектов после применения маппинга
        public List<OccupationViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Occupations.ToList()
                    .Where(x => x.Name.ToLower().Contains(search))
                    .ToList();

                return OccupationMapper.Map(result);
            }
        }

        //Получает объект из базы данных по указанному id и возвращает его после применения маппинга
        public OccupationViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Occupations.FirstOrDefault(x => x.ID == id);
                return OccupationMapper.Map(item);
            }
        }

        //Добавляет новый объект в базу данных. Производит маппинг перед добавлением, сохраняет изменения и возвращает добавленный объект после применения маппинга
        public OccupationViewModel Add(OccupationViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = OccupationMapper.Map(viewModel);

                context.Occupations.Add(entity);
                context.SaveChanges();

                return OccupationMapper.Map(entity);
            }
        }

        //Обновляет существующий объект в базе данных. Ищет объект по указанному id сохраняет изменения и возвращает объект после применения маппинга
        public OccupationViewModel Update(OccupationViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Occupations.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;

                context.SaveChanges();

                return OccupationMapper.Map(entity);
            }
        }

        //удаляет объект из базы данных по указанному id. Ищет объект для удаления, выполняет маппинг перед удалением, удаляет объект из базы данных
        //и возвращает удаленный объект после применения маппинга
        public OccupationViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Occupations.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                OccupationViewModel viewModel = OccupationMapper.Map(entity);

                context.Occupations.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
