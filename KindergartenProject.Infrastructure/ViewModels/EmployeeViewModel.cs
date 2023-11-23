using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class EmployeeViewModel
    {
        public long ID { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string DateOfBirth { get; set; }

        public string FullName { get; set; }

        public long Experience { get; set; }

        public long PostId { get; set; }

        public long UserId { get; set; }

        public string PostName { get; set; }

        public string UserName { get; set; }
    }
}
