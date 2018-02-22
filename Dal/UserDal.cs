using System;
using PlantInquiry.Models.Db;
using XASoft.CommonModel.ExceptionModel;
using XASoft.EfHelper.Models;
using XASoft.Extensions;

namespace PlantInquiry.Dal
{

    public partial class UserDal : DalBase<User, DataBase>
    {
        public UserDal() : base(new DataBase())
        {
        }
        public bool IsExistUsername(string username)
        {
            return SingleOrDefault(i => i.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) != null;
        }

        public User Login(string username, string pwd)
        {
            var user = SingleOrDefault(i => i.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && i.Pwd == pwd.CalculateMd5WithSalt(Configs.Md5Salt));
            if (user == null)
            {
                throw new MsgException("�û������������");
            }
            else
            {
                return user;
            }
        }

        public void SignUp(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Pwd))
            {
                throw new MsgException("�û��������벻��Ϊ��");
            }
            if (string.IsNullOrEmpty(user.NickName))
            {
                throw new MsgException("�ǳƲ���Ϊ��");
            }
            if (user.NickName.Length > 8)
            {
                throw new MsgException("�ǳƲ��ܳ���8���ַ�");
            }
            if (!IsExistUsername(user.Username))
            {
                throw new MsgException("�û����Ѵ���");
            }
            user.RoleType = RoleTypeEnum.User;
            user.Pwd = user.Pwd.CalculateMd5WithSalt(Configs.Md5Salt);
            base.Db.User.Add(user);
            base.Db.SaveChanges();
        }

    }
}
