using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class KidViewModel
    {
        public long ID { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        public long GroupId { get; set; }

        public string GroupName { get; set; }
    }
}

