using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class KindergartenRepository : IBaseRepository<KindergartenViewModel>
    {
        public List<KindergartenViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Kindergartens.ToList();
                return KindergartenMapper.Map(items);
            }
        }

        public KindergartenViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Kindergartens.FirstOrDefault(x => x.ID == id);
                return KindergartenMapper.Map(item);
            }
        }

        public KindergartenViewModel Add(KindergartenViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = KindergartenMapper.Map(viewModel);

                context.Kindergartens.Add(entity);
                context.SaveChanges();

                return KindergartenMapper.Map(entity);
            }
        }

        public KindergartenViewModel Update(KindergartenViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Kindergartens.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;
                entity.Address = viewModel.Address;
                entity.StartTime = viewModel.StartTime;
                entity.EndTime = viewModel.EndTime;

                context.SaveChanges();

                return KindergartenMapper.Map(entity);
            }
        }

        public KindergartenViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Kindergartens.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                KindergartenViewModel viewModel = KindergartenMapper.Map(entity);

                context.Kindergartens.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
