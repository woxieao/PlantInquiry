using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XASoft.EfHelper.Models;

namespace PlantInquiry.Models.Db
{
    [Table("Problem")]
    public partial class Problem : DbBase
    {

        [Required]
        [StringLength(255)]
        public string ProblemName { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ProblemDes { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ProblemSol { get; set; }

        [Required]
        [StringLength(255)]
        public string ProblemVega { get; set; }

        [Required]
        [StringLength(255)]
        public string ProblemType { get; set; }

        [StringLength(4000)]
        public string ImgList { get; set; }

    }
}
