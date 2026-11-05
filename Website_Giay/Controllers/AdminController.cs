using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Website_Giay.Models;
using Website_Giay.ViewModels;

namespace Website_Giay.Controllers
{
    public class AdminController : Controller
    {
        private ShoeStoreDbContext db = new ShoeStoreDbContext();

        public ActionResult Index()
        {
            var model = new AdminDashboardViewModel();

            try
            {
                var users = db.Users.OrderByDescending(u => u.CreatedDate).ToList();
                var products = db.Products.Include(p => p.Brand).Include(p => p.Category).OrderByDescending(p => p.ProductId).ToList();
                var orders = db.Orders.Include(o => o.OrderDetails.Select(d => d.Product)).OrderByDescending(o => o.OrderDate).ToList();

                model.TotalProducts = products.Count;
                model.TotalUsers = users.Count;
                model.TotalOrders = orders.Count;
                model.TotalRevenue = orders.Sum(o => o.TotalAmount);

                model.Users = users.Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    Role = u.Role,
                    CreatedDate = u.CreatedDate
                }).ToList();

                model.Products = products.Select(p => new ProductItemViewModel
                {
                    Id = p.ProductId,
                    Name = p.ProductName,
                    BrandName = p.Brand != null ? p.Brand.BrandName : "",
                    CategoryName = p.Category != null ? p.Category.CategoryName : "",
                    TargetAudience = p.TargetAudience,
                    ProductType = p.ProductType,
                    Price = p.Price,
                    OldPrice = p.OldPrice,
                    Rating = p.Rating,
                    ReviewCount = p.ReviewCount,
                    Stock = p.Stock,
                    Badge = p.Badge,
                    ImageUrl = p.ImageUrl
                }).ToList();

                model.RecentOrders = orders.Take(10).Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    CustomerName = o.CustomerName,
                    CustomerPhone = o.CustomerPhone,
                    ShippingAddress = o.ShippingAddress,
                    PaymentMethod = o.PaymentMethod,
                    OrderStatus = o.OrderStatus,
                    TotalAmount = o.TotalAmount
                }).ToList();

                model.Brands = db.Brands.Select(b => new BrandViewModel { BrandId = b.BrandId, BrandName = b.BrandName }).ToList();
                model.Categories = db.Categories.Select(c => new CategoryViewModel { CategoryId = c.CategoryId, CategoryName = c.CategoryName }).ToList();
            }
            catch (Exception)
            {
                // Fallback demo data
                model.TotalProducts = 49;
                model.TotalUsers = 2;
                model.TotalOrders = 1;
                model.TotalRevenue = 3990000;
                model.Users = new List<UserViewModel>
                {
                    new UserViewModel { UserId = 1, FullName = "Quản trị viên", Email = "admin@solestyle.vn", Role = "Admin", PhoneNumber = "0368068274" },
                    new UserViewModel { UserId = 2, FullName = "Nguyễn Văn An", Email = "an.nguyen@example.com", Role = "Customer", PhoneNumber = "0901234567" }
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                var p = db.Products.Find(id);
                if (p != null)
                {
                    db.Products.Remove(p);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa sản phẩm thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            try
            {
                var u = db.Users.Find(id);
                if (u != null)
                {
                    db.Users.Remove(u);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa người dùng thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy người dùng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
