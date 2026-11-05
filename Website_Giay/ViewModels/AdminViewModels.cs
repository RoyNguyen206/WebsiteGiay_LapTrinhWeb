using System.Collections.Generic;

namespace Website_Giay.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public List<ProductItemViewModel> Products { get; set; } = new List<ProductItemViewModel>();
        public List<OrderViewModel> RecentOrders { get; set; } = new List<OrderViewModel>();
        public List<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
