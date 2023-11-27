using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class RoleRepository : IBaseRepository<RoleViewModel>
    {
        public List<RoleViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Roles.ToList();
                return RoleMapper.Map(items);
            }
        }

        public RoleViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Roles.FirstOrDefault(x => x.ID == id);
                return RoleMapper.Map(item);
            }
        }

        public List<RoleViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Roles.ToList()
                    .Where(x => x.Name.ToLower().Contains(search))
                    .ToList();

                return RoleMapper.Map(result);
            }
        }

        public RoleViewModel Add(RoleViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = RoleMapper.Map(viewModel);

                context.Roles.Add(entity);
                context.SaveChanges();

                return RoleMapper.Map(entity);
            }
        }

        public RoleViewModel Update(RoleViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Roles.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;

                context.SaveChanges();

                return RoleMapper.Map(entity);
            }
        }

        public RoleViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Roles.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                RoleViewModel viewModel = RoleMapper.Map(entity);

                context.Roles.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
