using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class GroupViewModel
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public long AvailableSeats { get; set; }

        public long EmployeeId { get; set; }

        public string EmployeeName { get; set; }
    }
}
