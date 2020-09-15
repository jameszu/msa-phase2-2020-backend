using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CanvasAPI.Models
{
    public class Poll
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Poll_ID { get; set; }

        [Required]
        public string Pool_Title { get; set; }

        public ICollection<PollOption> PollOption { get; set; }


    }
}
