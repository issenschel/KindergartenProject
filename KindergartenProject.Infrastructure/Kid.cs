namespace KindergartenProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kid")]
    public partial class KidEntity
    {
        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Surname { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        [StringLength(2147483647)]
        public string Patronymic { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string DateOfBirth { get; set; }

        [Column("Group_ID")]
        public long GroupId { get; set; }

        public virtual GroupEntity Group { get; set; }
    }
}
