using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CanvasAPI.Models
{
    public class PollOption
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Option_ID { get; set; }

        [Required]
        public int Poll_ID { get; set; }
        [Required]
        public string Text { get; set; }
        public ICollection<Vote> Vote { get; set; }


    }
}
