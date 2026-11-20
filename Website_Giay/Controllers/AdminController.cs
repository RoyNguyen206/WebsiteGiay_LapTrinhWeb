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
        private ShoeStoreDBEntities db = new ShoeStoreDBEntities();

        // Dữ liệu mẫu lưu trữ trong bộ nhớ cho các chức năng mở rộng
        private static List<AdminReviewViewModel> staticReviews = null;
        private static List<AdminDiscountViewModel> staticDiscounts = null;
        private static List<AdminBannerViewModel> staticBanners = null;
        private static List<AdminContactViewModel> staticContacts = null;
        private static AdminWebsiteSettingsViewModel staticSettings = null;

        public AdminController()
        {
            // Khởi tạo dữ liệu mẫu lần đầu nếu chưa có
            if (staticReviews == null)
            {
                staticReviews = new List<AdminReviewViewModel>
                {
                    new AdminReviewViewModel { ReviewId = 1, CustomerName = "Nguyễn Văn An", ProductName = "Nike Air Force 1 '07", Rating = 5, Comment = "Giày đi rất êm, form đẹp chuẩn size, giao hàng cực nhanh!", CreatedDate = DateTime.Now.AddDays(-2), IsApproved = true },
                    new AdminReviewViewModel { ReviewId = 2, CustomerName = "Trần Thị Bình", ProductName = "Adidas Ultraboost Light", Rating = 5, Comment = "Đế boost êm ái thích hợp chạy bộ và tập gym.", CreatedDate = DateTime.Now.AddDays(-4), IsApproved = true },
                    new AdminReviewViewModel { ReviewId = 3, CustomerName = "Lê Hoàng Nam", ProductName = "Puma Suede Classic", Rating = 4, Comment = "Màu sắc đúng như hình chụp, chất da lộn đẹp.", CreatedDate = DateTime.Now.AddDays(-6), IsApproved = true },
                    new AdminReviewViewModel { ReviewId = 4, CustomerName = "Phạm Quốc Tuấn", ProductName = "New Balance 530", Rating = 3, Comment = "Hộp hơi móp khi vận chuyển nhưng giày bên trong vẫn ổn.", CreatedDate = DateTime.Now.AddDays(-8), IsApproved = false }
                };
            }

            if (staticDiscounts == null)
            {
                staticDiscounts = new List<AdminDiscountViewModel>
                {
                    new AdminDiscountViewModel { DiscountId = 1, Code = "SOLESTYLE10", DiscountPercent = 10, DiscountAmount = 0, UsageLimit = 100, UsedCount = 28, ExpiryDate = DateTime.Now.AddMonths(1), IsActive = true },
                    new AdminDiscountViewModel { DiscountId = 2, Code = "GIAM50K", DiscountPercent = 0, DiscountAmount = 50000, UsageLimit = 200, UsedCount = 89, ExpiryDate = DateTime.Now.AddMonths(2), IsActive = true },
                    new AdminDiscountViewModel { DiscountId = 3, Code = "BLACKFRIDAY", DiscountPercent = 20, DiscountAmount = 0, UsageLimit = 50, UsedCount = 50, ExpiryDate = DateTime.Now.AddDays(-5), IsActive = false }
                };
            }

            if (staticBanners == null)
            {
                staticBanners = new List<AdminBannerViewModel>
                {
                    new AdminBannerViewModel { BannerId = 1, Title = "BST Nike Air Max Mới Nhất", SubTitle = "Đẳng cấp thể thao đỉnh cao", ImageUrl = "~/assets/img/nike-purple.png", LinkUrl = "/Product?brand=Nike", DisplayOrder = 1, IsActive = true },
                    new AdminBannerViewModel { BannerId = 2, Title = "Adidas Ultraboost Thế Hệ Mới", SubTitle = "Êm ái trên từng bước chạy", ImageUrl = "~/assets/img/adidas.png", LinkUrl = "/Product?brand=Adidas", DisplayOrder = 2, IsActive = true },
                    new AdminBannerViewModel { BannerId = 3, Title = "Converse Chuck Taylor Classic", SubTitle = "Phong cách đường phố bất hủ", ImageUrl = "~/assets/img/red-converse.png", LinkUrl = "/Product?brand=Converse", DisplayOrder = 3, IsActive = true }
                };
            }

            if (staticContacts == null)
            {
                staticContacts = new List<AdminContactViewModel>
                {
                    new AdminContactViewModel { ContactId = 1, FullName = "Võ Minh Khang", Email = "khang.vo@gmail.com", PhoneNumber = "0938111222", Subject = "Hỏi về chính sách đổi size giày", Message = "Cho em hỏi shop có hỗ trợ đổi size tận nơi không ạ?", CreatedDate = DateTime.Now.AddDays(-1), IsReplied = true },
                    new AdminContactViewModel { ContactId = 2, FullName = "Đặng Thị Thảo", Email = "thao.dang@gmail.com", PhoneNumber = "0977888999", Subject = "Tư vấn chọn giày chạy bộ", Message = "Mình muốn tìm đôi giày chạy êm, hỗ trợ tốt cho cổ chân.", CreatedDate = DateTime.Now.AddHours(-5), IsReplied = false }
                };
            }

            if (staticSettings == null)
            {
                staticSettings = new AdminWebsiteSettingsViewModel();
            }
        }

        // ==========================================
        // 1. TRANG TỔNG QUAN & DỮ LIỆU CÁC TAB
        // ==========================================
        public ActionResult Index()
        {
            var model = new AdminDashboardViewModel();

            try
            {
                var users = db.Users.OrderByDescending(u => u.CreatedDate).ToList();
                var products = db.Products.Include(p => p.Brand).Include(p => p.Category).OrderByDescending(p => p.ProductId).ToList();
                var orders = db.Orders.Include(o => o.OrderDetails.Select(d => d.Product)).OrderByDescending(o => o.OrderDate).ToList();

                // Thống kê nhanh
                model.TotalProducts = products.Count;
                model.TotalUsers = users.Count;
                model.TotalOrders = orders.Count;
                model.TotalRevenue = orders.Where(o => o.OrderStatus != "Đã hủy").Sum(o => (decimal?)o.TotalAmount) ?? 0m;
                model.LowStockCount = products.Count(p => p.Stock <= 10);
                model.PendingOrdersCount = orders.Count(o => o.OrderStatus == "Đang xử lý");

                // Danh sách người dùng
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

                // Danh sách sản phẩm
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

                // Danh sách đơn hàng chi tiết
                model.Orders = orders.Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    CustomerName = o.CustomerName,
                    CustomerPhone = o.CustomerPhone,
                    ShippingAddress = o.ShippingAddress,
                    PaymentMethod = o.PaymentMethod,
                    OrderStatus = o.OrderStatus,
                    TotalAmount = o.TotalAmount,
                    Details = o.OrderDetails.Select(d => new OrderDetailItemViewModel
                    {
                        ProductId = d.ProductId,
                        ProductName = d.Product != null ? d.Product.ProductName : ("Sản phẩm #" + d.ProductId),
                        ImageUrl = d.Product != null ? d.Product.ImageUrl : "~/assets/img/default.png",
                        SelectedColor = d.SelectedColor,
                        SelectedSize = d.SelectedSize,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                    }).ToList()
                }).ToList();

                // Danh sách thương hiệu và danh mục
                model.Brands = db.Brands.Select(b => new BrandViewModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    LogoUrl = b.LogoUrl,
                    Description = b.Description
                }).ToList();

                model.Categories = db.Categories.Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Slug = c.Slug,
                    Description = c.Description
                }).ToList();

                // Quản lý kho hàng
                model.InventoryItems = products.Select(p => new AdminInventoryItemViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    BrandName = p.Brand != null ? p.Brand.BrandName : "",
                    CategoryName = p.Category != null ? p.Category.CategoryName : "",
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Stock = p.Stock,
                    IsLowStock = p.Stock <= 10
                }).OrderBy(x => x.Stock).ToList();

                // Top sản phẩm bán chạy (dựa vào số lượng tồn kho thấp hoặc lượt mua)
                model.TopSellingProducts = products.Take(5).Select(p => new AdminTopProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ImageUrl = p.ImageUrl,
                    BrandName = p.Brand != null ? p.Brand.BrandName : "",
                    Price = p.Price,
                    TotalSold = Math.Max(5, 50 - p.Stock),
                    TotalRevenue = Math.Max(5, 50 - p.Stock) * p.Price
                }).OrderByDescending(x => x.TotalSold).ToList();
            }
            catch (Exception)
            {
                // Fallback nếu có lỗi
                model.TotalProducts = 0;
                model.TotalUsers = 0;
                model.TotalOrders = 0;
                model.TotalRevenue = 0;
            }

            // Gán dữ liệu đánh giá, khuyến mãi, banner, liên hệ, cài đặt
            model.Reviews = staticReviews.OrderByDescending(r => r.ReviewId).ToList();
            model.Discounts = staticDiscounts.OrderByDescending(d => d.DiscountId).ToList();
            model.Banners = staticBanners.OrderBy(b => b.DisplayOrder).ToList();
            model.Contacts = staticContacts.OrderByDescending(c => c.ContactId).ToList();
            model.WebsiteSettings = staticSettings;

            return View(model);
        }

        // ==========================================
        // 2. QUẢN LÝ ĐƠN HÀNG (STT 1)
        // ==========================================
        [HttpPost]
        public JsonResult UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                var order = db.Orders.Find(orderId);
                if (order != null)
                {
                    order.OrderStatus = status;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Đã cập nhật trạng thái đơn hàng #" + orderId + " thành: " + status });
                }
                return Json(new { success = false, message = "Không tìm thấy đơn hàng #" + orderId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteOrder(int id)
        {
            try
            {
                var order = db.Orders.Find(id);
                if (order != null)
                {
                    // Xóa chi tiết đơn hàng trước
                    var details = db.OrderDetails.Where(d => d.OrderId == id).ToList();
                    db.OrderDetails.RemoveRange(details);

                    db.Orders.Remove(order);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Đã hủy và xóa đơn hàng #" + id + " thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy đơn hàng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // ==========================================
        // 3. QUẢN LÝ KHO HÀNG (STT 2)
        // ==========================================
        [HttpPost]
        public JsonResult UpdateStock(int productId, int stock)
        {
            try
            {
                var product = db.Products.Find(productId);
                if (product != null)
                {
                    product.Stock = Math.Max(0, stock);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Cập nhật số lượng tồn kho thành công!", newStock = product.Stock, isLowStock = product.Stock <= 10 });
                }
                return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // ==========================================
        // 4. QUẢN LÝ DANH MỤC & THƯƠNG HIỆU (STT 3 & 4)
        // ==========================================
        [HttpPost]
        public JsonResult AddCategory(string categoryName, string description)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    return Json(new { success = false, message = "Tên danh mục không được để trống." });
                }

                string slug = categoryName.ToLower().Trim().Replace(" ", "-");
                var cat = new Category
                {
                    CategoryName = categoryName.Trim(),
                    Slug = slug,
                    Description = description
                };
                db.Categories.Add(cat);
                db.SaveChanges();

                return Json(new { success = true, message = "Thêm danh mục mới thành công!", categoryId = cat.CategoryId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteCategory(int id)
        {
            try
            {
                var cat = db.Categories.Find(id);
                if (cat != null)
                {
                    bool hasProducts = db.Products.Any(p => p.CategoryId == id);
                    if (hasProducts)
                    {
                        return Json(new { success = false, message = "Không thể xóa vì danh mục đang có sản phẩm!" });
                    }

                    db.Categories.Remove(cat);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa danh mục thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy danh mục." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AddBrand(string brandName, string logoUrl, string description)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(brandName))
                {
                    return Json(new { success = false, message = "Tên thương hiệu không được để trống." });
                }

                var brand = new Brand
                {
                    BrandName = brandName.Trim(),
                    LogoUrl = string.IsNullOrWhiteSpace(logoUrl) ? "~/assets/img/default.png" : logoUrl.Trim(),
                    Description = description
                };
                db.Brands.Add(brand);
                db.SaveChanges();

                return Json(new { success = true, message = "Thêm thương hiệu mới thành công!", brandId = brand.BrandId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteBrand(int id)
        {
            try
            {
                var brand = db.Brands.Find(id);
                if (brand != null)
                {
                    bool hasProducts = db.Products.Any(p => p.BrandId == id);
                    if (hasProducts)
                    {
                        return Json(new { success = false, message = "Không thể xóa vì thương hiệu đang có sản phẩm!" });
                    }

                    db.Brands.Remove(brand);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa thương hiệu thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy thương hiệu." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // ==========================================
        // 5. QUẢN LÝ SẢN PHẨM
        // ==========================================
        [HttpPost]
        public JsonResult AddProduct(string productName, int brandId, int categoryId, decimal price, int stock, string description, string targetAudience, string productType, string imageUrl)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productName))
                {
                    return Json(new { success = false, message = "Tên sản phẩm không được để trống." });
                }

                var p = new Product
                {
                    ProductName = productName.Trim(),
                    BrandId = brandId,
                    CategoryId = categoryId,
                    Price = price,
                    OldPrice = price,
                    Stock = stock,
                    Description = description,
                    TargetAudience = string.IsNullOrEmpty(targetAudience) ? "Unisex" : targetAudience,
                    ProductType = string.IsNullOrEmpty(productType) ? "Giày" : productType,
                    ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? "~/assets/img/default.png" : imageUrl.Trim(),
                    IsActive = true,
                    IsFeatured = false,
                    Rating = 5.0,
                    ReviewCount = 0,
                    CreatedDate = DateTime.Now
                };

                db.Products.Add(p);
                db.SaveChanges();

                return Json(new { success = true, message = "Thêm sản phẩm mới thành công!", productId = p.ProductId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
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

        // ==========================================
        // 6. QUẢN LÝ TÀI KHOẢN
        // ==========================================
        [HttpPost]
        public JsonResult AddUser(string fullName, string email, string password, string phoneNumber, string address, string role)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return Json(new { success = false, message = "Email và mật khẩu không được để trống." });
                }

                if (db.Users.Any(u => u.Email == email.Trim()))
                {
                    return Json(new { success = false, message = "Email này đã được sử dụng." });
                }

                var user = new User
                {
                    FullName = fullName,
                    Email = email.Trim(),
                    Password = password,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Role = string.IsNullOrEmpty(role) ? "Customer" : role,
                    CreatedDate = DateTime.Now
                };

                db.Users.Add(user);
                db.SaveChanges();

                return Json(new { success = true, message = "Tạo tài khoản thành công!", userId = user.UserId });
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

        // ==========================================
        // 7. QUẢN LÝ MÃ GIẢM GIÁ (STT 5)
        // ==========================================
        [HttpPost]
        public JsonResult AddDiscount(string code, int percent, decimal amount, int limit, string expiryDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    return Json(new { success = false, message = "Vui lòng nhập mã giảm giá." });
                }

                DateTime exp = DateTime.Now.AddDays(30);
                if (!string.IsNullOrEmpty(expiryDate))
                {
                    DateTime.TryParse(expiryDate, out exp);
                }

                int newId = staticDiscounts.Any() ? staticDiscounts.Max(d => d.DiscountId) + 1 : 1;
                var discount = new AdminDiscountViewModel
                {
                    DiscountId = newId,
                    Code = code.Trim().ToUpper(),
                    DiscountPercent = percent,
                    DiscountAmount = amount,
                    UsageLimit = limit > 0 ? limit : 100,
                    UsedCount = 0,
                    ExpiryDate = exp,
                    IsActive = true
                };

                staticDiscounts.Add(discount);
                return Json(new { success = true, message = "Đã tạo mã giảm giá " + discount.Code + " thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteDiscount(int id)
        {
            try
            {
                var d = staticDiscounts.FirstOrDefault(x => x.DiscountId == id);
                if (d != null)
                {
                    staticDiscounts.Remove(d);
                    return Json(new { success = true, message = "Đã xóa mã giảm giá thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy mã giảm giá." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // ==========================================
        // 8. QUẢN LÝ ĐÁNH GIÁ (STT 6)
        // ==========================================
        [HttpPost]
        public JsonResult ToggleReview(int reviewId, bool isApproved)
        {
            try
            {
                var r = staticReviews.FirstOrDefault(x => x.ReviewId == reviewId);
                if (r != null)
                {
                    r.IsApproved = isApproved;
                    return Json(new { success = true, message = isApproved ? "Đã duyệt hiển thị đánh giá!" : "Đã ẩn đánh giá này!" });
                }
                return Json(new { success = false, message = "Không tìm thấy đánh giá." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteReview(int id)
        {
            try
            {
                var r = staticReviews.FirstOrDefault(x => x.ReviewId == id);
                if (r != null)
                {
                    staticReviews.Remove(r);
                    return Json(new { success = true, message = "Đã xóa đánh giá thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy đánh giá." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // ==========================================
        // 9. CÀI ĐẶT WEBSITE & LIÊN HỆ
        // ==========================================
        [HttpPost]
        public JsonResult SaveWebsiteSettings(AdminWebsiteSettingsViewModel settings)
        {
            try
            {
                if (settings != null)
                {
                    staticSettings = settings;
                    return Json(new { success = true, message = "Lưu cài đặt website thành công!" });
                }
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ToggleContactReply(int id)
        {
            try
            {
                var c = staticContacts.FirstOrDefault(x => x.ContactId == id);
                if (c != null)
                {
                    c.IsReplied = !c.IsReplied;
                    return Json(new { success = true, message = c.IsReplied ? "Đã đánh dấu là ĐÃ PHẢN HỒI" : "Đã đánh dấu là CHƯA PHẢN HỒI", isReplied = c.IsReplied });
                }
                return Json(new { success = false, message = "Không tìm thấy tin nhắn liên hệ." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteContact(int id)
        {
            try
            {
                var c = staticContacts.FirstOrDefault(x => x.ContactId == id);
                if (c != null)
                {
                    staticContacts.Remove(c);
                    return Json(new { success = true, message = "Đã xóa tin nhắn liên hệ thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy liên hệ." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ToggleBanner(int id)
        {
            try
            {
                var b = staticBanners.FirstOrDefault(x => x.BannerId == id);
                if (b != null)
                {
                    b.IsActive = !b.IsActive;
                    return Json(new { success = true, message = b.IsActive ? "Đã bật hiển thị banner!" : "Đã tắt hiển thị banner!", isActive = b.IsActive });
                }
                return Json(new { success = false, message = "Không tìm thấy banner." });
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
