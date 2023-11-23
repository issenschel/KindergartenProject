using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class ModeOfTheDayViewModel
    {
        public long ID { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public long OccupationId { get; set; }

        public string OccupationName { get; set; }

        public long GroupId { get; set; }

        public string GroupName { get; set; }
    }
}
