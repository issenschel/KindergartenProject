namespace KindergartenProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("employee")]
    public partial class EmployeeEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeEntity()
        {
            Group = new HashSet<GroupEntity>();
        }

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

        public long Experience { get; set; }

        [Column("Post_ID")]
        public long PostId { get; set; }

        [Column("User_ID")]
        public long UserId { get; set; }

        public virtual PostEntity Post { get; set; }

        public virtual UserEntity User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupEntity> Group { get; set; }
    }
}
