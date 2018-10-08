using System.ComponentModel;

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserGroup")]
    public partial class UserGroup
    {
        [StringLength(20)]
        [Display(Name = "ID")]
        public string ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên nhóm")]
        public string Name { get; set; }
    }
}
