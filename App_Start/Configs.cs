
using PlantInquiry.Controllers;
using XASoft.BaseMvc;

namespace PlantInquiry
{
    public class Configs
    {
        
        public static readonly string UserInfoSessionKeyName = nameof(UserInfoSessionKeyName);
        public static readonly string VerificationCodeKeyName = nameof(VerificationCodeKeyName);
        public static readonly string Md5Salt = nameof(Md5Salt);
        public static readonly string LoginPage = "Home/Login";
        public static readonly int MatchWordMaxLength = 20;
   }
}

