using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    public class EmployeeRepository
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
    }
}
