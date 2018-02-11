using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XASoft.EfHelper.Models;

namespace PlantInquiry.Models.Db
{
    [Table("Element")]
    public partial class Element : DbBase
    {

        [Column("Element")]
        [Required]
        [StringLength(255)]
        public string Element1 { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ElementEffect { get; set; }

        [Column(TypeName = "text")]
        public string ElementShortageEffect { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ElementSol { get; set; }
    }
}
