using System;
using System.Collections.Generic;

namespace Website_Giay.ViewModels
{
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string TargetAudience { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public int Stock { get; set; }
        public string Badge { get; set; }
        public string ImageUrl { get; set; }
        public bool HasDiscount => OldPrice > Price && Price > 0;
        public int DiscountPercent => HasDiscount ? (int)Math.Round((OldPrice - Price) / OldPrice * 100) : 0;
    }

    public class ProductColorViewModel
    {
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string TargetAudience { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public int Stock { get; set; }
        public string Badge { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool HasDiscount => OldPrice > Price && Price > 0;
        public int DiscountPercent => HasDiscount ? (int)Math.Round((OldPrice - Price) / OldPrice * 100) : 0;

        public List<ProductColorViewModel> Colors { get; set; } = new List<ProductColorViewModel>();
        public List<string> Sizes { get; set; } = new List<string>();
        public List<ProductItemViewModel> RelatedProducts { get; set; } = new List<ProductItemViewModel>();
    }

    public class BrandFilterViewModel
    {
        public string BrandName { get; set; }
        public string LogoUrl { get; set; }
        public int Count { get; set; }
        public bool IsSelected { get; set; }
    }

    public class CategoryFilterViewModel
    {
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public int Count { get; set; }
        public bool IsSelected { get; set; }
    }

    public class ProductListViewModel
    {
        public List<ProductItemViewModel> Products { get; set; } = new List<ProductItemViewModel>();
        public List<BrandFilterViewModel> Brands { get; set; } = new List<BrandFilterViewModel>();
        public List<CategoryFilterViewModel> Categories { get; set; } = new List<CategoryFilterViewModel>();
        public List<string> Audiences { get; set; } = new List<string> { "Nam", "Nữ", "Unisex" };

        public string SelectedBrand { get; set; }
        public string SelectedCategory { get; set; }
        public string SelectedAudience { get; set; }
        public string PriceRange { get; set; }
        public string SearchKeyword { get; set; }
        public string SortBy { get; set; }

        public int TotalItems { get; set; }
    }
}
