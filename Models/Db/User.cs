using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlantInquiry.Attributes;
using XASoft.EfHelper.Models;

namespace PlantInquiry.Models.Db
{
    public enum RoleTypeEnum
    {
        User = 1,
        Manager = 2,
        Professor = 3
    }
   
    [Table("User")]
    public partial class User : DbBase
    {
        public string NickName { get; set; }
        public string Username { get; set; }
        public string Motto { get; set; }
        public string Pwd { get; set; }
        public RoleTypeEnum RoleType { get; set; }
    }
}
