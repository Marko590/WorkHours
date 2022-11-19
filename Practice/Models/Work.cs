using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Models
{
    public class Work
    {
        public int WorkId { get; set; }
        public string WorkerName { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public float TotalHours { get; set; }
        public int WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public virtual Worker Worker { get; set; } = null!;

       
    }
}
