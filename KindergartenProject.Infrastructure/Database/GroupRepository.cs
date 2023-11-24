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
        public List<GroupViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Groups.ToList();
                return GroupMapper.Map(items);
            }
        }

        public GroupViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Groups.FirstOrDefault(x => x.ID == id);
                return GroupMapper.Map(item);
            }
        }

        public GroupViewModel Add(GroupViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = GroupMapper.Map(viewModel);

                context.Groups.Add(entity);
                context.SaveChanges();

                return GroupMapper.Map(entity);
            }
        }

        public GroupViewModel Update(GroupViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Groups.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;
                entity.AvailableSeats = viewModel.AvailableSeats;
                entity.EmployeeId = viewModel.EmployeeId;

                context.SaveChanges();

                return GroupMapper.Map(entity);
            }
        }

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
    }
}
