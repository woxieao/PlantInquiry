using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XASoft.EfHelper.Models;

namespace PlantInquiry.Models.Db
{
    [Table("MsgLog")]
    public partial class MsgLog : DbBase
    {
        [Column(TypeName = "text")]
        [Required]
        public string MsgContent { get; set; }

        public Guid UserId { get; set; }

        public Guid ToProfessorId { get; set; }
    }
}
