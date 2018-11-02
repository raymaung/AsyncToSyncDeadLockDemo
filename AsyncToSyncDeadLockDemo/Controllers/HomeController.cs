using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncToSyncDeadLockDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Log.Info("Index Page Requested ===>");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();

        }


        public ActionResult SyncPage()
        {
            ViewBag.Message = "Sync Page Here";

            MyPage myPage = new MyPage
            {
                Content = AsyncToSync.GetString()
            };

            ViewBag.MyPage = myPage;

            return View();
        }


        public async Task<ActionResult> AsyncPage()
        {
            ViewBag.Message = "Async Page Here";

            MyPage myPage = new MyPage
            {
                Content = await AsyncToSync.GetStringAsync()
            };

            ViewBag.MyPage = myPage;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class MyPage
    {
        public string Content { get; set; }
    }
}