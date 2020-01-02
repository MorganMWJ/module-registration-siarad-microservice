using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Models
{
    [Table("student")]
    public class Student
    {
        [Key]
        [Column("uid")]
        public virtual string Uid { get; set; }

        [Required]
        [Column("forename")]
        public virtual string Forename { get; set; }

        [Required]
        [Column("surname")]
        public virtual string Surname { get; set; }

        [JsonIgnore]
        public virtual ICollection<ModuleStudent> ModuleStudents { get; set; }
    }
}
