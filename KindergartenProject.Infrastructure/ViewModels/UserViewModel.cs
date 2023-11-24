using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class UserViewModel
    {
        public long ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public long RoleId { get; set; }

        public long RoleName { get; set; }
    }
}
