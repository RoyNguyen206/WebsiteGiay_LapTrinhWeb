
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Website_Giay.Models;

namespace Website_Giay.Controllers
{
    public class PromotionController : Controller
    {
        private ShoeStoreDBEntities db = new ShoeStoreDBEntities();

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Gallery()
        {
            DateTime hienTai = DateTime.Now;

            List<Promotion> danhSach = db.Promotions
                .Where(p =>
                    p.IsActive == true
                    &&
                    (
                        p.StartDate == null
                        ||
                        p.StartDate <= hienTai
                    )
                    &&
                    (
                        p.EndDate == null
                        ||
                        p.EndDate >= hienTai
                    )
                )
                .OrderBy(p => p.DisplayOrder)
                .ThenBy(p => p.PromotionId)
                .Take(4)
                .ToList();

            return PartialView(
                "_PromotionGallery",
                danhSach
            );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
