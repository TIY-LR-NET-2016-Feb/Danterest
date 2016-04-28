using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Danterest2.Web.Models;
using Humanizer;
using Microsoft.AspNet.Identity;

namespace Danterest2.Web.Controllers
{
    [Authorize]
    public class PinsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult GetAllMyPins()
        {
            var userid = User.Identity.GetUserId();

            var user = db.Users.Include(u => u.Dans).FirstOrDefault(u => u.Id == userid);

            var model = user.Dans.Select(d => new
            {
                d.PhotoUrl,
                d.LinkUrl,
                DanId = d.Id,
                CreatedOn = d.CreatedOn.Humanize(),
                d.Description,
                CreatedBy = d.Owner.UserName,
                NUmOfComments = d.Comments.Count()
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDanDetails(int danID)
        {
            var dan = db.Dans.Find(danID);
            if (dan == null)
                return HttpNotFound("No Dan Found.");
            var model = new 
            {
                dan.PhotoUrl,
                dan.LinkUrl,
                DanId = dan.Id,
                CreatedOn = dan.CreatedOn.Humanize(),
                dan.Description,
                CreatedBy = dan.Owner.UserName,
                NumOfComments = dan.Comments.Count(),
                Comments = dan.Comments.Select(c => new
                {
                    c.CreatedOn,
                    Author = c.CreatedBy.UserName,
                    CommentId = c.Id,
                    c.Text
                })
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SavePin(CreatePinVM model)
        {
            if (!ModelState.IsValid)
                return Json(new { Message = "You messed up" });


            var userid = User.Identity.GetUserId();
            var user = db.Users.Find(userid);


            var dan = new Dan()
            {
                Owner = user,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                PhotoUrl = model.PhotoUrl,
                LinkUrl = string.IsNullOrEmpty(model.LinkUrl) ? model.PhotoUrl : model.LinkUrl
            };


            db.Dans.Add(dan);
            db.SaveChanges();

            return Json(dan);
        }
    }
}