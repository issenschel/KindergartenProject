namespace KindergartenProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kindergarten")]
    public partial class KindergartenEntity
    {
        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Address { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string StartTime { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string EndTime { get; set; }
    }
}
