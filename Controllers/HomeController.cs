using System.Web.Mvc;
using PlantInquiry.Dal;
using XASoft.BaseMvc;


namespace PlantInquiry.Controllers
{
    public class HomeController : BaseApiController
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Chat_()
        {
            return View();
        }
        public ActionResult ManageVega_()
        {
            return View();
        }
        public ActionResult ProfessorChat_()
        {
            return View();
        }
        public ActionResult SearchResult(string keyword = "")
        {
            return View(new[] { keyword });
        }
        public ActionResult Main()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            return View(new ProblemDal().GetById(id));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}