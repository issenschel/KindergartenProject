using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class PostViewModel
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }

        public string WorkSchedule { get; set; }
    }
}
