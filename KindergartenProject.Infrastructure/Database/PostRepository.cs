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
        public List<PostViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Posts.ToList();
                return PostMapper.Map(items);
            }
        }

        public PostViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Posts.FirstOrDefault(x => x.ID == id);
                return PostMapper.Map(item);
            }
        }

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
