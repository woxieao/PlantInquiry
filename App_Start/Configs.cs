
using XASoft.BaseMvc;

namespace PlantInquiry
{
    public class Configs
    {
        public static readonly string UserInfoSessionKeyName = nameof(UserInfoSessionKeyName);
        public static readonly string LoginPage = "Home/Login";
        public static readonly int MatchWordMaxLength = 20;
        static Configs() {
            BaseApiController.HideUnknownException(false);
        }
    }
}

