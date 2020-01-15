using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Models
{
    [Table("module_staff")]
    public class ModuleStaff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("module_id")]
        public Module Module { get; set; }

        [Required]
        [Column("uid")]
        public Staff Staff { get; set; }
    }
}
