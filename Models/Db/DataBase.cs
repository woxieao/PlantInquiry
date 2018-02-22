using System.Data.Entity;
using XASoft.EfHelper.Models;

namespace PlantInquiry.Models.Db
{
    public partial class DataBase : DbContext
    {
        public DataBase()
            : base("name=DataBase")
        {
        }

        public virtual DbSet<Element> Element { get; set; }
        public virtual DbSet<MsgLog> MsgLog { get; set; }
        public virtual DbSet<Problem> Problem { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }

    }
}
