using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Models
{
    [Table("module_student")]
    public class ModuleStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("module_id")]
        public Module Module { get; set; }

        [Column("uid")]
        public Student Student { get; set; }
    }
}
