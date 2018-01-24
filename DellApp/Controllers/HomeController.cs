using DellApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace DellApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (DellAppDB db = new DellAppDB())
            {
                UserResults userResult = new UserResults();
                userResult.UserSys = db.UserSys.Where(p => p.Email.Equals(User.Identity.Name)).FirstOrDefault();

                if (userResult.UserSys.UserRoleId == 1) userResult.Customers = db.Customer.ToList();

                return View(userResult);
            }
        }

        public ActionResult GetCustomers(CustomersFilter filters)
        {
            using (DellAppDB db = new DellAppDB())
            {
                UserResults userResult = new UserResults();
                userResult.UserSys = db.UserSys.Where(p => p.Email.Equals(User.Identity.Name)).FirstOrDefault();

                if (userResult.UserSys.UserRoleId == 1) userResult.Customers = db.Customer.ToList();

                return View(userResult);
            }
        }
    }
}