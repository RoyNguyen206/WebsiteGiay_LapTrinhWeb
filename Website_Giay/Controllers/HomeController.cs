using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Website_Giay.Models;
using Website_Giay.ViewModels;

namespace Website_Giay.Controllers
{
    public class HomeController : Controller
    {
        private ShoeStoreDbContext db = new ShoeStoreDbContext();

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            try
            {
                // Query brands
                var brands = db.Brands.ToList();
                model.Brands = brands.Select(b => new BrandViewModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    LogoUrl = b.LogoUrl,
                    Description = b.Description
                }).ToList();

                // Query categories
                var categories = db.Categories.ToList();
                model.Categories = categories.Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Slug = c.Slug,
                    Description = c.Description
                }).ToList();

                // Query all active products
                var productsQuery = db.Products.Include(p => p.Brand).Include(p => p.Category).Where(p => p.IsActive);
                var allProducts = productsQuery.ToList();

                model.TotalProducts = allProducts.Count;

                // Map to ProductItemViewModel
                var productVms = allProducts.Select(p => MapToItemViewModel(p)).ToList();

                model.AllProducts = productVms;

                // Banner products (featured or selected)
                model.BannerProducts = productVms.Where(p => p.Id == 1 || p.Id == 2 || p.Id == 11 || p.Id == 3).Take(3).ToList();
                if (!model.BannerProducts.Any())
                {
                    model.BannerProducts = productVms.Take(3).ToList();
                }

                // Flash Deal products (products with discount OldPrice > Price)
                model.FlashDealProducts = productVms.Where(p => p.HasDiscount).Take(8).ToList();
                if (!model.FlashDealProducts.Any())
                {
                    model.FlashDealProducts = productVms.Take(6).ToList();
                }

                // New Arrivals
                model.NewArrivals = productVms.Where(p => p.Badge != null && p.Badge.ToLower().Contains("new")).Take(8).ToList();
                if (!model.NewArrivals.Any())
                {
                    model.NewArrivals = productVms.OrderByDescending(p => p.Id).Take(8).ToList();
                }
            }
            catch (Exception)
            {
                // Fallback in case DB is not yet created or connection failed
                model = GetFallbackHomeData();
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "SoleStyle - Cửa hàng giày sneaker chính hãng hàng đầu";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Liên hệ với SoleStyle";
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static ProductItemViewModel MapToItemViewModel(Product p)
        {
            return new ProductItemViewModel
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
            };
        }

        private HomeViewModel GetFallbackHomeData()
        {
            var fallback = new HomeViewModel();
            fallback.Brands = new List<BrandViewModel>
            {
                new BrandViewModel { BrandId = 1, BrandName = "Nike", LogoUrl = "~/assets/img/logoNike.png" },
                new BrandViewModel { BrandId = 2, BrandName = "Adidas", LogoUrl = "~/assets/img/logoAdidas.png" },
                new BrandViewModel { BrandId = 3, BrandName = "Converse", LogoUrl = "~/assets/img/logoConverse.png" },
                new BrandViewModel { BrandId = 4, BrandName = "Puma", LogoUrl = "~/assets/img/logoPuma.png" },
                new BrandViewModel { BrandId = 5, BrandName = "New Balance", LogoUrl = "~/assets/img/logoNewBalance.png" }
            };

            fallback.Categories = new List<CategoryViewModel>
            {
                new CategoryViewModel { CategoryId = 1, CategoryName = "Giày", Slug = "giay" },
                new CategoryViewModel { CategoryId = 2, CategoryName = "Dép", Slug = "dep" },
                new CategoryViewModel { CategoryId = 3, CategoryName = "Nón", Slug = "non" },
                new CategoryViewModel { CategoryId = 4, CategoryName = "Phụ kiện", Slug = "phu-kien" }
            };

            var sampleProducts = new List<ProductItemViewModel>
            {
                new ProductItemViewModel { Id = 1, Name = "Giày Nike Air Jordan 1 Retro High OG", BrandName = "Nike", CategoryName = "Giày", TargetAudience = "Nam", ProductType = "Sneakers bóng rổ", Price = 3990000, OldPrice = 4590000, Rating = 4.9, ReviewCount = 18, Stock = 6, Badge = "new", ImageUrl = "~/assets/img/Untitled - June 20, 2026 at 02.06.14 (4) (16).png" },
                new ProductItemViewModel { Id = 2, Name = "Giày Adidas Ultraboost 2026 Running", BrandName = "Adidas", CategoryName = "Giày", TargetAudience = "Unisex", ProductType = "Giày chạy bộ", Price = 4290000, OldPrice = 0, Rating = 4.8, ReviewCount = 12, Stock = 9, Badge = "new", ImageUrl = "~/assets/img/image (5).png" },
                new ProductItemViewModel { Id = 3, Name = "Chuck Taylor All Star", BrandName = "Converse", CategoryName = "Giày", TargetAudience = "Unisex", ProductType = "Giày cổ cao", Price = 1700000, OldPrice = 0, Rating = 5.0, ReviewCount = 20, Stock = 20, Badge = "Kinh điển", ImageUrl = "~/assets/img/image (4).png" },
                new ProductItemViewModel { Id = 4, Name = "Nike P-6000", BrandName = "Nike", CategoryName = "Giày", TargetAudience = "Unisex", ProductType = "Lifestyle", Price = 2890000, OldPrice = 3200000, Rating = 4.7, ReviewCount = 8, Stock = 15, Badge = "sale", ImageUrl = "~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (10).png" },
                new ProductItemViewModel { Id = 6, Name = "Dép Adidas Adilette Comfort", BrandName = "Adidas", CategoryName = "Dép", TargetAudience = "Unisex", ProductType = "Dép quai ngang", Price = 850000, OldPrice = 950000, Rating = 4.6, ReviewCount = 25, Stock = 18, Badge = "hot", ImageUrl = "~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (18).png" },
                new ProductItemViewModel { Id = 9, Name = "Giày New Balance 530 Essential", BrandName = "New Balance", CategoryName = "Giày", TargetAudience = "Unisex", ProductType = "Running retro", Price = 2450000, OldPrice = 2790000, Rating = 4.9, ReviewCount = 31, Stock = 12, Badge = "new", ImageUrl = "~/assets/img/mr530asm_nb_03_i.webp" }
            };

            fallback.AllProducts = sampleProducts;
            fallback.BannerProducts = sampleProducts.Take(3).ToList();
            fallback.FlashDealProducts = sampleProducts.Where(p => p.HasDiscount).ToList();
            fallback.NewArrivals = sampleProducts;
            fallback.TotalProducts = sampleProducts.Count;

            return fallback;
        }
    }
}
