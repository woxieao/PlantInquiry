using System.Collections.Generic;
using System.Linq;
using PlantInquiry.Models.Db;
using XASoft.EfHelper.Models;

namespace PlantInquiry.Dal
{

    public partial class BannerDal : DalBase<Banner, DataBase>
    {
        public BannerDal() : base(new DataBase())
        {
        }
        public List<Banner> GetBannerList()
        {
            return this.Where(i => !i.DelFlag).OrderByDescending(i=>i.Sort).ThenByDescending(i=>i.CreateDate).ToList();
        }
    }
}
