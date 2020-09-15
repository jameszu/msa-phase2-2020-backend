using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CanvasAPI.Models
{
    public class Vote
    {
        [Required]
        [Key]
        public int User_ID { get; set; }

        [Required]
        public int Poll_ID { get; set; }

    }
}
