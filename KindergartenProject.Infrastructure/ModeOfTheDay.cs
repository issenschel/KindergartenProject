namespace KindergartenProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("modeOfTheDay")]
    public partial class ModeOfTheDayEntity
    {
        public long ID { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        [Column("Occupation_ID")]
        public long OccupationId { get; set; }

        [Column("Group_ID")]
        public long GroupId { get; set; }

        public virtual GroupEntity Group { get; set; }

        public virtual OccupationEntity Occupation { get; set; }
    }
}
