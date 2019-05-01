using MaerskLine.CMS.Models;
using MaerskLine.CMS.Models.Constants;
using MaerskLine.CMS.Models.Entities;
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
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create()
        {
            using (var db = ApplicationDbContext.Create())
            {
                return View(new ShippingInfoViewModel { ItemCategories = await db.ItemCategories.ToListAsync() });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShippingInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = ApplicationDbContext.Create())
            {
                var order = new Order
                {
                    FromAddress = model.FromAddress,
                    ToAddress = model.ToAddress,
                    ItemCategoryId = model.ItemCategoryId,
                    ItemName = model.ItemName,
                    ItemWeight = model.ItemWeight,
                    PickupDate = model.PickupDate,
                    Status = ShippingStatus.OrderReceived,
                    OrderTrackingId = Guid.NewGuid().ToString(),
                    UserId = this.User.Identity.GetUserId()
                };
                db.Orders.Add(order);
                db.Notifications.Add(new Notification
                {
                    DateTime = DateTime.Now,
                    Order = order,
                    Message = string.Format("Your shipping order with tracking ID {0} has been created.", order.OrderTrackingId)
                });
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return View(model);
                }
                return RedirectToAction("Index","Home");
            }
        }

        [Authorize(Roles = "APort, DPort")]
        public async Task<ActionResult> Management()
        {
            using (var db = ApplicationDbContext.Create())
            {
                var orders = await db.Orders.OrderByDescending(x => x.PickupDate).ToListAsync();
                return View(orders);
            }
        }

        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> MyOrderHistory()
        {
            using (var db = ApplicationDbContext.Create())
            {
                var userId = this.User.Identity.GetUserId();
                var orders = await db.Orders.Where(x => x.UserId == userId).OrderByDescending(x => x.PickupDate).ToListAsync();
                return View(orders);
            }
        }

        public async Task<ActionResult> View(long orderId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var order = await db.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

                if (order == null || (this.User.IsInRole("Customer") && order.UserId != this.User.Identity.GetUserId()))
                {
                    return new HttpStatusCodeResult(404);
                }

                await db.SaveChangesAsync();
                return View(new ShippingInfoViewModel
                {
                    FromAddress = order.FromAddress,
                    ToAddress = order.ToAddress,
                    PickupDate = order.PickupDate,
                    ItemCategoryId = order.ItemCategoryId,
                    ItemName = order.ItemName,
                    OrderId = order.Id,
                    ItemWeight = order.ItemWeight,
                    ItemCategories = await db.ItemCategories.ToListAsync(),
                    Status = order.Status, 
                    OrderTrackingId = order.OrderTrackingId
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "APort, DPort")]
        public async Task<ActionResult> UpdateStatus(ShippingInfoViewModel model)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var order = await db.Orders.FirstOrDefaultAsync(x => x.Id == model.OrderId);

                if (order == null)
                {
                    return new HttpStatusCodeResult(404);
                }

                model.ItemCategories = await db.ItemCategories.ToListAsync();

                order.Status = this.User.IsInRole("APort") ? ShippingStatus.Arrived : ShippingStatus.Delivering;

                db.Notifications.Add(new Notification
                {
                    DateTime = DateTime.Now,
                    Message = string.Format("Your shipping order with tracking ID {0} has arrived the {1} port.",order.OrderTrackingId, this.User.IsInRole("APort") ? "arrival" : "departure"),
                    OrderId = model.OrderId
                });

                await db.SaveChangesAsync();

                model.Status = order.Status;

                return View("View",model);
            }
        }
    }
}