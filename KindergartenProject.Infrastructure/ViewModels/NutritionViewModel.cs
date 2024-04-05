using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenProject.Infrastructure.ViewModels
{
    public class NutritionViewModel
    {
        public long ID { get; set; }

        public long BreakFast { get; set; }

        public long Brunch { get; set; }

        public long Lunch { get; set; }

        public long AfternoonSnack { get; set; }

        public long Dinner { get; set; }

        public string BreakFastName { get; set; }

        public string BrunchName { get; set; }

        public string LunchName { get; set; }

        public string AfternoonSnackName { get; set; }

        public string DinnerName { get; set; }
    }
}
