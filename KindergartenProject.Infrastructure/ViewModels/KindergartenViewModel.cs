using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class KindergartenViewModel
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
