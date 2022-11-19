using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Models
{
    public class Coordinator
    {
        [Key]
        public string Username { get; set; } = null!;
        [Column(TypeName = "VARCHAR(100)")]
        public string Name { get; set; } = null!;

    }
}
