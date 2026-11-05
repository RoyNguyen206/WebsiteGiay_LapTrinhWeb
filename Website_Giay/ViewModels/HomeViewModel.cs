using System.Collections.Generic;

namespace Website_Giay.ViewModels
{
    public class BrandViewModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
    }

    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }

    public class HomeViewModel
    {
        public List<ProductItemViewModel> BannerProducts { get; set; } = new List<ProductItemViewModel>();
        public List<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<ProductItemViewModel> FlashDealProducts { get; set; } = new List<ProductItemViewModel>();
        public List<ProductItemViewModel> NewArrivals { get; set; } = new List<ProductItemViewModel>();
        public List<ProductItemViewModel> AllProducts { get; set; } = new List<ProductItemViewModel>();
        public int TotalProducts { get; set; }
    }
}
