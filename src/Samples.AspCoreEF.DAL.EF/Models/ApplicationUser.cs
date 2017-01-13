using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.DAL.EF.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        public string FullName { set; get; }
        [MaxLength(256)]
        public string Address { set; get; }
        public DateTime? BirthDay { set; get; }

    }
}
