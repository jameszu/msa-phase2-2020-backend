using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CanvasAPI.Models
{
    public class ColorData
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorDataID { get; set; }

        [Required]
        public int RowIndex { get; set; }

        [Required]
        public int ColumnIndex { get; set; }

        [Required]
        public string Hex { get; set; }

        [Required]
        public int CanvasID { get; set; }
    }
}
