using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.Database
{
    internal interface IBaseRepository<TViewModel>
    {
        TViewModel GetById(long id);
        List<TViewModel> GetList();
        TViewModel Update(TViewModel model);
        TViewModel Delete(long id);
        TViewModel Add(TViewModel model);
        List <TViewModel> Search(string search);
    }
}
