using System;
using System.Threading.Tasks;
using System.Web.Hosting;
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
            Log.Info("AsyncPage ==>");
            ViewBag.Message = "Async Page Here";

            MyPage myPage = new MyPage
            {
                Content = await AsyncToSync.GetStringAsync()
            };

            ViewBag.MyPage = myPage;

            return View();
        }


        public ActionResult QBWIPage()
        {
            Log.Info("QBQIPage ==>");
            ViewBag.Message = "QBWI Page Here";

            
            HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    for (int i = 0; i < 20; i++)
                    {
                        var duration = i * 1000;
                        AsyncToSync.GetStringAsync(duration)
                            .ContinueWith(t => { Log.Info($"QBWI - StringAsync Completed duration: #{duration}"); }, cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    Log.Info("QueueBackgroundWorkItem - Exception", e);
                }
            });

            MyPage myPage = new MyPage
            {
                Content = "QBWI - Page Here"
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