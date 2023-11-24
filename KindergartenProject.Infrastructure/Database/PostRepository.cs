using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class PostRepository
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
    }
}
