using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using PlantInquiry.Core;
using PlantInquiry.Dal;
using PlantInquiry.Models;
using PlantInquiry.Models.Db;
using XASoft.BaseMvc;
using XASoft.Extensions;

namespace PlantInquiry.Controllers
{
    public class ApiController : BaseApiController
    {
        static ApiController()
        {
            //    HideUnknownException(false);
        }
        private static readonly List<Problem> ProblemList = MethodCacheExtensions.GetCache(new ProblemDal().GetProblemList);
        public OkResult Login(string vCode, User user)
        {
            Session[Configs.UserInfoSessionKeyName] = 1;
            return Ok();
        }
        public OkResult GetProblemList(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            Func<string, List<ProblemSearchResult>> func = str =>
            {
                return ProblemList.Select(problem => Logic.IsContain(keyword, problem)).ToList();
            };
            var result = MethodCacheExtensions.GetCache(func, keyword).Where(i => i.IsMatch() || string.IsNullOrEmpty(keyword)).OrderByDescending(i => i.PriorityLevel).ThenByDescending(i => i.StringSearchList.Count);
            return Ok(new
            {
                List = result.Skip(pageSize * (pageIndex - 1)).Take(pageSize),
                TotalCount = result.Count(),
                TotalPage = Math.Ceiling((result.Count() / (double)pageSize))
            });
        }

        public OkResult GetBannerList()
        {
            return Ok(new BannerDal().GetBannerList());
        }

        private void CreateCheckCodeImage(string vCode)
        {
            using (var image = new Bitmap((int)Math.Ceiling(vCode.Length * 24.5), 39))
            {
                using (var g = Graphics.FromImage(image))
                {
                    var random = new Random();
                    g.Clear(Color.AntiqueWhite);
                    for (var i = 0; i < 20; i++)
                    {
                        var x1 = random.Next(image.Width);
                        var x2 = random.Next(image.Width);
                        var y1 = random.Next(image.Height);
                        var y2 = random.Next(image.Height);
                        g.DrawLine(new Pen(Color.FromArgb(random.Next(250), random.Next(250), random.Next(250))), x1, y1, x2, y2);
                    }
                    var font = new Font("Jokerman", 20, FontStyle.Bold | FontStyle.Italic);
                    var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                        Color.DarkBlue, Color.Green, 1.2f, true);
                    g.DrawString(vCode, font, brush, 2, 2);
                    for (var i = 0; i < 80; i++)
                    {
                        var x = random.Next(image.Width);
                        var y = random.Next(image.Height);
                        image.SetPixel(x, y, Color.FromArgb(random.Next(250), random.Next(250), random.Next(250)));
                    }
                    g.DrawRectangle(new Pen(Color.DarkGray), 0, 0, image.Width - 1, image.Height - 1);
                    var ms = new MemoryStream();
                    image.Save(ms, ImageFormat.Png);
                    Response.ClearContent();
                    Response.ContentType = "image/Png";
                    Response.BinaryWrite(ms.ToArray());
                }
            }
        }

        public void CreateVerificationCode()
        {
            var vCode = Guid.NewGuid().ToString().Substring(32);
            Session[Configs.VerificationCodeKeyName] = vCode;
            CreateCheckCodeImage(vCode);
        }

        public OkResult SignUp(string vCode, User user)
        {
            
            new UserDal().SignUp(user);
            return Ok();
        }
    }
}
