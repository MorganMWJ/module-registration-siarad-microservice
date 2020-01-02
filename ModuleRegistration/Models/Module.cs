using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Models
{
    [Table("module")]
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public virtual int Id { get; set; }

        [Required]
        [Column("code")]
        public virtual string Code { get; set; }

        [Required]
        [Column("year")]
        public virtual string Year { get; set; }

        [Required]
        [Column("class_code")]
        public virtual string ClassCode { get; set; }

        [Required]
        [Column("coordinator_uid")]
        public virtual string CoordinatorUid { get; set; } //Module Coordinator

        [Required]
        [Column("title")]
        public virtual string Title { get; set; }

        [JsonIgnore]
        public virtual ICollection<ModuleStaff> ModuleStaff { get; set; }

        [JsonIgnore]
        public virtual ICollection<ModuleStudent> ModuleStudents { get; set; }
    }
}
