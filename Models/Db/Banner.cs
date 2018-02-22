using System.ComponentModel.DataAnnotations.Schema;
using XASoft.EfHelper.Models;

namespace PlantInquiry.Models.Db
{

    [Table("Banner")]
    public partial class Banner : DbBase
    {
        public int Sort { get; set; }
        public string ImgUrl { get; set; }
        public string Title { get; set; }
    }
}
