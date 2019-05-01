using MaerskLine.CMS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MaerskLine.CMS.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("Customer"))
            {
                using (var db = ApplicationDbContext.Create())
                {
                    var userId = this.User.Identity.GetUserId();
                    var notifications = await db.Notifications.Where(x => x.Order.UserId == userId).OrderByDescending(x => x.DateTime).Include(x => x.Order).ToListAsync();
                    return View(notifications);
                }
            }
            else if (User.IsInRole("DPort") || User.IsInRole("APort"))
            {
                return RedirectToAction("Management", "Order");
            }
            return View();
        }
    }
}