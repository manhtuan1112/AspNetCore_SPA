using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.DAL.EF.Models
{
    [Table("ApplicationRoleGroups")]
    public class ApplicationRoleGroup
    {

        public long GroupId { set; get; }

        public string RoleId { set; get; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroup { set; get; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { set; get; }

    }
}
