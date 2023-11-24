using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class EmployeeRepository : IBaseRepository<EmployeeViewModel>
    {
        public List<EmployeeViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Employees.ToList();
                return EmployeeMapper.Map(items);
            }
        }

        public EmployeeViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Employees.FirstOrDefault(x => x.ID == id);
                return EmployeeMapper.Map(item);
            }
        }

        public EmployeeViewModel Add(EmployeeViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = EmployeeMapper.Map(viewModel);

                context.Employees.Add(entity);
                context.SaveChanges();

                return EmployeeMapper.Map(entity);
            }
        }

        public EmployeeViewModel Update(EmployeeViewModel viewModel)
        {
            using (var context = new Context())
            {
                var entity = context.Employees.FirstOrDefault(x => x.ID == viewModel.ID);
                if (entity == null)
                    return null;

                entity.Name = viewModel.Name;
                entity.Surname = viewModel.Surname;
                entity.Patronymic = viewModel.Patronymic;
                entity.DateOfBirth = viewModel.DateOfBirth;
                entity.Experience = viewModel.Experience;
                entity.UserId = viewModel.UserId;
                entity.PostId = viewModel.PostId;

                context.SaveChanges();

                return EmployeeMapper.Map(entity);
            }
        }

        public EmployeeViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var entity = context.Employees.FirstOrDefault(x => x.ID == id);
                if (entity == null)
                    return null;

                EmployeeViewModel viewModel = EmployeeMapper.Map(entity);

                context.Employees.Remove(entity);
                context.SaveChanges();

                return viewModel;
            }
        }
    }
}
