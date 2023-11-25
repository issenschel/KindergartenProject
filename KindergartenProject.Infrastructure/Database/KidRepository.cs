using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class KidRepository : IBaseRepository<KidViewModel>
    {
        public List<KidViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Kids.ToList();
                return KidMapper.Map(items);
            }
        }

        public KidViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Kids.FirstOrDefault(x => x.ID == id);
                return KidMapper.Map(item);
            }
        }

        public List<GroupViewModel> GetGroups()
        {
            using (var context = new Context())
            {
                var items = context.Groups.ToList();
                return GroupMapper.Map(items);
            }
        }

        public KidViewModel Add(KidViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = KidMapper.Map(viewModel);

                context.Kids.Add(entity);
                context.SaveChanges();

                return KidMapper.Map(entity);
            }
        }

        public KidViewModel Update(KidViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Kids.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;
                entity.Surname = viewModel.Surname;
                entity.Patronymic = viewModel.Patronymic;
                entity.DateOfBirth = viewModel.DateOfBirth;
                entity.GroupId = viewModel.GroupId;

                context.SaveChanges();

                return KidMapper.Map(entity);
            }
        }

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
