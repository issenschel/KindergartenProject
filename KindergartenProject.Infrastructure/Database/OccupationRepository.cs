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
        public List<OccupationViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Occupations.ToList();
                return OccupationMapper.Map(items);
            }
        }

        public OccupationViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Occupations.FirstOrDefault(x => x.ID == id);
                return OccupationMapper.Map(item);
            }
        }

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
