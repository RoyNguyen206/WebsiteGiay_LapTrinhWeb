
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_Giay.Models;

namespace Website_Giay.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminPromotionController : Controller
    {
        private readonly ShoeStoreDBEntities db =
            new ShoeStoreDBEntities();

        // DANH SACH SU KIEN
        [HttpGet]
        public ActionResult Index()
        {
            var danhSach = db.Promotions
                .OrderBy(p => p.DisplayOrder)
                .ThenByDescending(p => p.PromotionId)
                .ToList();

            return View(danhSach);
        }

        // THEM SU KIEN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            string Title,
            string Description,
            DateTime? StartDate,
            DateTime? EndDate,
            int DisplayOrder = 0,
            bool IsActive = false,
            HttpPostedFileBase ImageFile = null
        )
        {
            if (string.IsNullOrWhiteSpace(Title)
                || Title.Trim().Length > 200)
            {
                TempData["Error"] =
                    "Tiêu đề phải có từ 1 đến 200 ký tự.";

                return RedirectToAction("Index");
            }

            if (Description != null
                && Description.Length > 1000)
            {
                TempData["Error"] =
                    "Mô tả không được quá 1000 ký tự.";

                return RedirectToAction("Index");
            }

            if (StartDate.HasValue
                && EndDate.HasValue
                && StartDate.Value > EndDate.Value)
            {
                TempData["Error"] =
                    "Ngày kết thúc phải sau ngày bắt đầu.";

                return RedirectToAction("Index");
            }

            string duongDanAnh;
            string loi;

            if (!LuuAnh(ImageFile, out duongDanAnh, out loi))
            {
                TempData["Error"] = loi;
                return RedirectToAction("Index");
            }

            Promotion suKien = new Promotion
            {
                Title = Title.Trim(),
                Description = Description,
                ImageUrl = duongDanAnh,
                StartDate = StartDate,
                EndDate = EndDate,
                DisplayOrder = DisplayOrder,
                IsActive = IsActive,
                CreatedDate = DateTime.Now
            };

            db.Promotions.Add(suKien);
            db.SaveChanges();

            TempData["Success"] =
                "Thêm sự kiện thành công!";

            return RedirectToAction("Index");
        }

        // CAP NHAT SU KIEN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            int PromotionId,
            string Title,
            string Description,
            DateTime? StartDate,
            DateTime? EndDate,
            int DisplayOrder = 0,
            bool IsActive = false,
            HttpPostedFileBase ImageFile = null
        )
        {
            Promotion suKien = db.Promotions
                .Find(PromotionId);

            if (suKien == null)
            {
                return HttpNotFound();
            }

            if (string.IsNullOrWhiteSpace(Title)
                || Title.Trim().Length > 200)
            {
                TempData["Error"] =
                    "Tiêu đề phải có từ 1 đến 200 ký tự.";

                return RedirectToAction("Index");
            }

            if (Description != null
                && Description.Length > 1000)
            {
                TempData["Error"] =
                    "Mô tả không được quá 1000 ký tự.";

                return RedirectToAction("Index");
            }

            if (StartDate.HasValue
                && EndDate.HasValue
                && StartDate.Value > EndDate.Value)
            {
                TempData["Error"] =
                    "Ngày kết thúc phải sau ngày bắt đầu.";

                return RedirectToAction("Index");
            }

            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string duongDanAnh;
                string loi;

                if (!LuuAnh(
                    ImageFile,
                    out duongDanAnh,
                    out loi))
                {
                    TempData["Error"] = loi;

                    return RedirectToAction("Index");
                }

                suKien.ImageUrl = duongDanAnh;
            }

            suKien.Title = Title.Trim();
            suKien.Description = Description;
            suKien.StartDate = StartDate;
            suKien.EndDate = EndDate;
            suKien.DisplayOrder = DisplayOrder;
            suKien.IsActive = IsActive;

            db.SaveChanges();

            TempData["Success"] =
                "Cập nhật sự kiện thành công!";

            return RedirectToAction("Index");
        }

        // XOA SU KIEN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Promotion suKien = db.Promotions.Find(id);

            if (suKien == null)
            {
                return HttpNotFound();
            }

            db.Promotions.Remove(suKien);
            db.SaveChanges();

            TempData["Success"] =
                "Đã xóa sự kiện thành công!";

            return RedirectToAction("Index");
        }

        // LUU ANH TU MAY TINH
        private bool LuuAnh(
            HttpPostedFileBase file,
            out string duongDanAnh,
            out string thongBaoLoi
        )
        {
            duongDanAnh = null;
            thongBaoLoi = null;

            if (file == null || file.ContentLength <= 0)
            {
                thongBaoLoi =
                    "Vui lòng chọn hình ảnh sự kiện.";

                return false;
            }

            if (file.ContentLength > 5 * 1024 * 1024)
            {
                thongBaoLoi =
                    "Kích thước ảnh tối đa là 5MB.";

                return false;
            }

            string duoiFile = Path.GetExtension(
                file.FileName
            ).ToLowerInvariant();

            string[] duoiChoPhep =
            {
                ".jpg", ".jpeg", ".png", ".webp"
            };

            if (!duoiChoPhep.Contains(duoiFile))
            {
                thongBaoLoi =
                    "Chỉ chấp nhận JPG, PNG hoặc WEBP.";

                return false;
            }

            // KIEM TRA CHU KY FILE ANH
            byte[] header = new byte[12];

            int soByte = file.InputStream.Read(
                header, 0, header.Length
            );

            file.InputStream.Position = 0;

            bool laJpeg =
                soByte >= 3
                && header[0] == 0xFF
                && header[1] == 0xD8
                && header[2] == 0xFF;

            bool laPng =
                soByte >= 8
                && header[0] == 0x89
                && header[1] == 0x50
                && header[2] == 0x4E
                && header[3] == 0x47
                && header[4] == 0x0D
                && header[5] == 0x0A
                && header[6] == 0x1A
                && header[7] == 0x0A;

            bool laWebp =
                soByte >= 12
                && header[0] == 'R'
                && header[1] == 'I'
                && header[2] == 'F'
                && header[3] == 'F'
                && header[8] == 'W'
                && header[9] == 'E'
                && header[10] == 'B'
                && header[11] == 'P';

            bool hopLe =
                ((duoiFile == ".jpg"
                    || duoiFile == ".jpeg") && laJpeg)
                || (duoiFile == ".png" && laPng)
                || (duoiFile == ".webp" && laWebp);

            if (!hopLe)
            {
                thongBaoLoi =
                    "Nội dung file không đúng định dạng ảnh.";

                return false;
            }

            string thuMuc = Server.MapPath(
                "~/assets/img/promotions/"
            );

            if (!Directory.Exists(thuMuc))
            {
                Directory.CreateDirectory(thuMuc);
            }

            string tenFile =
                Guid.NewGuid().ToString("N") + duoiFile;

            string duongDanVatLy =
                Path.Combine(thuMuc, tenFile);

            file.SaveAs(duongDanVatLy);

            duongDanAnh =
                "~/assets/img/promotions/" + tenFile;

            return true;
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
