using System;
using System.Collections.Generic;

namespace Website_Giay.ViewModels
{
    // Model cho tổng quan Admin
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalRevenue { get; set; }
        public int LowStockCount { get; set; }
        public int PendingOrdersCount { get; set; }

        // Dữ liệu hiển thị trong các tab
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public List<ProductItemViewModel> Products { get; set; } = new List<ProductItemViewModel>();
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public List<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<AdminInventoryItemViewModel> InventoryItems { get; set; } = new List<AdminInventoryItemViewModel>();
        public List<AdminReviewViewModel> Reviews { get; set; } = new List<AdminReviewViewModel>();
        public List<AdminDiscountViewModel> Discounts { get; set; } = new List<AdminDiscountViewModel>();
        public List<AdminBannerViewModel> Banners { get; set; } = new List<AdminBannerViewModel>();
        public List<AdminContactViewModel> Contacts { get; set; } = new List<AdminContactViewModel>();
        public AdminWebsiteSettingsViewModel WebsiteSettings { get; set; } = new AdminWebsiteSettingsViewModel();
        public List<AdminTopProductViewModel> TopSellingProducts { get; set; } = new List<AdminTopProductViewModel>();
    }

    // Quản lý kho hàng
    public class AdminInventoryItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsLowStock { get; set; }
    }

    // Quản lý đánh giá
    public class AdminReviewViewModel
    {
        public int ReviewId { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
    }

    // Quản lý mã giảm giá
    public class AdminDiscountViewModel
    {
        public int DiscountId { get; set; }
        public string Code { get; set; }
        public int DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }

    // Quản lý banner trang chủ
    public class AdminBannerViewModel
    {
        public int BannerId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }

    // Quản lý liên hệ khách hàng
    public class AdminContactViewModel
    {
        public int ContactId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsReplied { get; set; }
    }

    // Cài đặt website
    public class AdminWebsiteSettingsViewModel
    {
        public string StoreName { get; set; } = "SoleStyle Sneaker Store";
        public string Hotline { get; set; } = "0368 068 274";
        public string Email { get; set; } = "hotro@solestyle.vn";
        public string Address { get; set; } = "123 Đường Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh";
        public decimal FreeShippingThreshold { get; set; } = 1500000;
        public string Announcement { get; set; } = "Chào mừng bạn đến với SoleStyle - Giày thể thao chính hãng!";
        public bool IsStoreOpen { get; set; } = true;
    }

    // Thống kê sản phẩm bán chạy
    public class AdminTopProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
