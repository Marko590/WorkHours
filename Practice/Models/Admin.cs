using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Models
{
    public class Admin
    {
        [Key]
        public string Username { get; set; } = null!;
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Name { get; set; }

    }
}
