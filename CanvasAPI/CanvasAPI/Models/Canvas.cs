using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CanvasAPI.Models
{
    public class Canvas
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int CanvasID { get; set; }

        // foreign key to Color Data
        public ICollection<ColorData> ColorData { get; set; }
    }
}
