using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Models
{
    [Table("staff")]
    public class Staff
    {
        [Key]
        [Column("uid")]
        public virtual string Uid { get; set; }

        [Column("forename")]
        public virtual string Forename { get; set; }

        [Column("surname")]
        public virtual string Surname { get; set; }

        [JsonIgnore]
        public virtual ICollection<ModuleStaff> ModuleStaff { get; set; }
    }
}
