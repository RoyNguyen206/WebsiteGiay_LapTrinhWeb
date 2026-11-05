using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Website_Giay.Models;
using Website_Giay.ViewModels;

namespace Website_Giay.Controllers
{
    public class ProductController : Controller
    {
        private ShoeStoreDbContext db = new ShoeStoreDbContext();

        private const int PageSize = 12;

        public ActionResult Index(string category, string brand, string audience, string price, string search, string sort, int page = 1)
        {
            if (page < 1) page = 1;

            var model = new ProductListViewModel
            {
                SelectedCategory = IsAll(category) ? null : category,
                SelectedBrand = IsAll(brand) ? null : brand,
                SelectedAudience = audience,
                PriceRange = IsAll(price) ? null : price,
                SearchKeyword = search,
                SortBy = sort
            };

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = PageSize;

            try
            {
                var query = db.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Where(p => p.IsActive);

                if (model.SelectedCategory != null)
                {
                    var cat = model.SelectedCategory.Trim().ToLower();
                    query = query.Where(p => p.Category.CategoryName.ToLower() == cat
                                          || p.Category.Slug.ToLower() == cat);
                }

                if (model.SelectedBrand != null)
                {
                    var br = model.SelectedBrand.Trim().ToLower();
                    query = query.Where(p => p.Brand.BrandName.ToLower() == br);
                }

                if (!string.IsNullOrEmpty(audience))
                {
                    var aud = audience.Trim().ToLower();
                    query = query.Where(p => p.TargetAudience.ToLower() == aud || p.TargetAudience == "Unisex");
                }

                if (model.PriceRange != null)
                {
                    switch (model.PriceRange.ToLower())
                    {
                        case "duoi-2-trieu":
                            query = query.Where(p => p.Price < 2000000);
                            break;
                        case "tu-2-den-3-trieu":
                            query = query.Where(p => p.Price >= 2000000 && p.Price <= 3000000);
                            break;
                        case "tren-3-trieu":
                            query = query.Where(p => p.Price > 3000000);
                            break;

                        case "duoi-1tr":
                            query = query.Where(p => p.Price < 1000000);
                            break;
                        case "1tr-2tr":
                            query = query.Where(p => p.Price >= 1000000 && p.Price <= 2000000);
                            break;
                        case "2tr-3tr":
                            query = query.Where(p => p.Price > 2000000 && p.Price <= 3000000);
                            break;
                        case "tren-3tr":
                            query = query.Where(p => p.Price > 3000000);
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var keyword = search.Trim().ToLower();
                    query = query.Where(p => p.ProductName.ToLower().Contains(keyword)
                                          || (p.Description != null && p.Description.ToLower().Contains(keyword))
                                          || (p.ProductType != null && p.ProductType.ToLower().Contains(keyword))
                                          || (p.Brand != null && p.Brand.BrandName.ToLower().Contains(keyword)));
                }

                switch (sort)
                {
                    case "gia-tang":
                        query = query.OrderBy(p => p.Price).ThenByDescending(p => p.ProductId);
                        break;
                    case "gia-giam":
                        query = query.OrderByDescending(p => p.Price).ThenByDescending(p => p.ProductId);
                        break;
                    case "giam-gia":
                        query = query.Where(p => p.OldPrice > p.Price)
                                     .OrderByDescending(p => p.OldPrice - p.Price)
                                     .ThenByDescending(p => p.ProductId);
                        break;
                    case "moi-nhat":
                        query = query.OrderByDescending(p => p.ProductId);
                        break;
                    case "danh-gia":
                        query = query.OrderByDescending(p => p.Rating).ThenByDescending(p => p.ReviewCount);
                        break;
                    case "ban-chay":
                        query = query.OrderByDescending(p => p.ReviewCount).ThenByDescending(p => p.ProductId);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.Rating)
                                     .ThenByDescending(p => p.ReviewCount)
                                     .ThenByDescending(p => p.ProductId);
                        break;
                }

                model.TotalItems = query.Count();

                var pageItems = query.Take(page * PageSize).ToList();

                model.Products = pageItems.Select(p => new ProductItemViewModel
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

                var brandCounts = db.Products.Where(p => p.IsActive)
                    .GroupBy(p => p.BrandId)
                    .Select(g => new { Id = g.Key, Count = g.Count() })
                    .ToList();

                model.Brands = db.Brands.ToList().Select(b => new BrandFilterViewModel
                {
                    BrandName = b.BrandName,
                    LogoUrl = b.LogoUrl,
                    Count = brandCounts.Where(x => x.Id == b.BrandId).Select(x => x.Count).FirstOrDefault(),
                    IsSelected = model.SelectedBrand != null
                                 && string.Equals(model.SelectedBrand, b.BrandName, StringComparison.OrdinalIgnoreCase)
                }).ToList();

                var categoryCounts = db.Products.Where(p => p.IsActive)
                    .GroupBy(p => p.CategoryId)
                    .Select(g => new { Id = g.Key, Count = g.Count() })
                    .ToList();

                model.Categories = db.Categories.ToList().Select(c => new CategoryFilterViewModel
                {
                    CategoryName = c.CategoryName,
                    Slug = c.Slug,
                    Count = categoryCounts.Where(x => x.Id == c.CategoryId).Select(x => x.Count).FirstOrDefault(),
                    IsSelected = model.SelectedCategory != null
                                 && (string.Equals(model.SelectedCategory, c.CategoryName, StringComparison.OrdinalIgnoreCase)
                                     || string.Equals(model.SelectedCategory, c.Slug, StringComparison.OrdinalIgnoreCase))
                }).ToList();
            }
            catch (Exception)
            {
                model = BuildFallbackListWithFilters(model, page);
            }

            return View(model);
        }

        public ActionResult Detail(int id)
        {
            try
            {
                var p = db.Products
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Include(x => x.Colors)
                    .Include(x => x.Sizes)
                    .FirstOrDefault(x => x.ProductId == id && x.IsActive);

                if (p == null)
                {
                    return HttpNotFound();
                }

                var model = new ProductDetailViewModel
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
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    Colors = p.Colors.Select(c => new ProductColorViewModel
                    {
                        ColorName = c.ColorName,
                        HexCode = c.HexCode,
                        ImageUrl = c.ImageUrl
                    }).ToList(),
                    Sizes = p.Sizes.Select(s => s.SizeValue).ToList()
                };

                var related = db.Products
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Where(x => x.IsActive && x.ProductId != id && (x.CategoryId == p.CategoryId || x.BrandId == p.BrandId))
                    .Take(4)
                    .ToList();

                model.RelatedProducts = related.Select(r => new ProductItemViewModel
                {
                    Id = r.ProductId,
                    Name = r.ProductName,
                    BrandName = r.Brand != null ? r.Brand.BrandName : "",
                    CategoryName = r.Category != null ? r.Category.CategoryName : "",
                    TargetAudience = r.TargetAudience,
                    ProductType = r.ProductType,
                    Price = r.Price,
                    OldPrice = r.OldPrice,
                    Rating = r.Rating,
                    ReviewCount = r.ReviewCount,
                    Stock = r.Stock,
                    Badge = r.Badge,
                    ImageUrl = r.ImageUrl
                }).ToList();

                return View(model);
            }
            catch (Exception)
            {
                var fallbackDetail = GetFallbackProductDetail(id);
                return View(fallbackDetail);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static bool IsAll(string value)
        {
            return string.IsNullOrWhiteSpace(value) || value.Trim().ToLower() == "tat-ca";
        }

        private ProductListViewModel BuildFallbackListWithFilters(ProductListViewModel selected, int page)
        {
            var vm = GetFallbackProductList();

            vm.SelectedCategory = selected.SelectedCategory;
            vm.SelectedBrand = selected.SelectedBrand;
            vm.SelectedAudience = selected.SelectedAudience;
            vm.PriceRange = selected.PriceRange;
            vm.SearchKeyword = selected.SearchKeyword;
            vm.SortBy = selected.SortBy;

            foreach (var b in vm.Brands)
                b.IsSelected = vm.SelectedBrand != null && string.Equals(vm.SelectedBrand, b.BrandName, StringComparison.OrdinalIgnoreCase);
            foreach (var c in vm.Categories)
                c.IsSelected = vm.SelectedCategory != null
                    && (string.Equals(vm.SelectedCategory, c.CategoryName, StringComparison.OrdinalIgnoreCase)
                        || string.Equals(vm.SelectedCategory, c.Slug, StringComparison.OrdinalIgnoreCase));

            var list = vm.Products.AsEnumerable();

            if (vm.SelectedCategory != null)
            {
                var matched = vm.Categories.FirstOrDefault(c => c.IsSelected);
                var catName = matched != null ? matched.CategoryName : vm.SelectedCategory;
                list = list.Where(p => string.Equals(p.CategoryName, catName, StringComparison.OrdinalIgnoreCase));
            }
            if (vm.SelectedBrand != null)
                list = list.Where(p => string.Equals(p.BrandName, vm.SelectedBrand, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(vm.SelectedAudience))
                list = list.Where(p => string.Equals(p.TargetAudience, vm.SelectedAudience, StringComparison.OrdinalIgnoreCase) || p.TargetAudience == "Unisex");

            if (vm.PriceRange != null)
            {
                switch (vm.PriceRange.ToLower())
                {
                    case "duoi-2-trieu": list = list.Where(p => p.Price < 2000000); break;
                    case "tu-2-den-3-trieu": list = list.Where(p => p.Price >= 2000000 && p.Price <= 3000000); break;
                    case "tren-3-trieu": list = list.Where(p => p.Price > 3000000); break;
                    case "duoi-1tr": list = list.Where(p => p.Price < 1000000); break;
                    case "1tr-2tr": list = list.Where(p => p.Price >= 1000000 && p.Price <= 2000000); break;
                    case "2tr-3tr": list = list.Where(p => p.Price > 2000000 && p.Price <= 3000000); break;
                    case "tren-3tr": list = list.Where(p => p.Price > 3000000); break;
                }
            }

            if (!string.IsNullOrWhiteSpace(vm.SearchKeyword))
            {
                var kw = vm.SearchKeyword.Trim().ToLower();
                list = list.Where(p => (p.Name != null && p.Name.ToLower().Contains(kw))
                                    || (p.ProductType != null && p.ProductType.ToLower().Contains(kw))
                                    || (p.BrandName != null && p.BrandName.ToLower().Contains(kw)));
            }

            switch (vm.SortBy)
            {
                case "gia-tang": list = list.OrderBy(p => p.Price); break;
                case "gia-giam": list = list.OrderByDescending(p => p.Price); break;
                case "giam-gia": list = list.Where(p => p.OldPrice > p.Price).OrderByDescending(p => p.OldPrice - p.Price); break;
                case "moi-nhat": list = list.OrderByDescending(p => p.Id); break;
                case "danh-gia": list = list.OrderByDescending(p => p.Rating); break;
                case "ban-chay": list = list.OrderByDescending(p => p.ReviewCount); break;
                default: list = list.OrderByDescending(p => p.Rating).ThenByDescending(p => p.ReviewCount); break;
            }

            var all = list.ToList();
            vm.TotalItems = all.Count;
            vm.Products = all.Take(page * PageSize).ToList();
            return vm;
        }

        private ProductListViewModel GetFallbackProductList()
        {
            var vm = new ProductListViewModel();
            vm.Products = new List<ProductItemViewModel>
            {
                new ProductItemViewModel { Id = 1, Name = "Giày Nike Air Jordan 1 Retro High OG", BrandName = "Nike", CategoryName = "Giày", TargetAudience = "Nam", ProductType = "Sneakers bóng rổ", Price = 3990000, OldPrice = 4590000, Rating = 4.9, ReviewCount = 18, Stock = 6, Badge = "new", ImageUrl = "~/assets/img/Untitled - June 20, 2026 at 02.06.14 (4) (16).png" },
                new ProductItemViewModel { Id = 2, Name = "Giày Adidas Ultraboost 2026 Running", BrandName = "Adidas", CategoryName = "Giày", TargetAudience = "Unisex", ProductType = "Giày chạy bộ", Price = 4290000, OldPrice = 0, Rating = 4.8, ReviewCount = 12, Stock = 9, Badge = "new", ImageUrl = "~/assets/img/image (5).png" },
                new ProductItemViewModel { Id = 3, Name = "Chuck Taylor All Star", BrandName = "Converse", CategoryName = "Giày", TargetAudience = "Unisex", ProductType = "Giày cổ cao", Price = 1700000, OldPrice = 0, Rating = 5.0, ReviewCount = 20, Stock = 20, Badge = "Kinh điển", ImageUrl = "~/assets/img/image (4).png" },
                new ProductItemViewModel { Id = 4, Name = "Nike P-6000", BrandName = "Nike", CategoryName = "Giày", TargetAudience = "Unisex", ProductType = "Lifestyle", Price = 2890000, OldPrice = 3200000, Rating = 4.7, ReviewCount = 8, Stock = 15, Badge = "sale", ImageUrl = "~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (10).png" },
                new ProductItemViewModel { Id = 6, Name = "Dép Adidas Adilette Comfort", BrandName = "Adidas", CategoryName = "Dép", TargetAudience = "Unisex", ProductType = "Dép quai ngang", Price = 850000, OldPrice = 950000, Rating = 4.6, ReviewCount = 25, Stock = 18, Badge = "hot", ImageUrl = "~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (18).png" },
                new ProductItemViewModel { Id = 9, Name = "Giày New Balance 530 Essential", BrandName = "New Balance", CategoryName = "Giày", TargetAudience = "Unisex", ProductType = "Running retro", Price = 2450000, OldPrice = 2790000, Rating = 4.9, ReviewCount = 31, Stock = 12, Badge = "new", ImageUrl = "~/assets/img/mr530asm_nb_03_i.webp" }
            };
            vm.TotalItems = vm.Products.Count;

            vm.Brands = new List<BrandFilterViewModel>
            {
                new BrandFilterViewModel { BrandName = "Nike", LogoUrl = "~/assets/img/logoNike.png", Count = 15 },
                new BrandFilterViewModel { BrandName = "Adidas", LogoUrl = "~/assets/img/logoAdidas.png", Count = 12 },
                new BrandFilterViewModel { BrandName = "Converse", LogoUrl = "~/assets/img/logoConverse.png", Count = 8 },
                new BrandFilterViewModel { BrandName = "Puma", LogoUrl = "~/assets/img/logoPuma.png", Count = 7 },
                new BrandFilterViewModel { BrandName = "New Balance", LogoUrl = "~/assets/img/logoNewBalance.png", Count = 7 }
            };

            vm.Categories = new List<CategoryFilterViewModel>
            {
                new CategoryFilterViewModel { CategoryName = "Giày", Slug = "giay", Count = 32 },
                new CategoryFilterViewModel { CategoryName = "Dép", Slug = "dep", Count = 6 },
                new CategoryFilterViewModel { CategoryName = "Nón", Slug = "non", Count = 5 },
                new CategoryFilterViewModel { CategoryName = "Phụ kiện", Slug = "phu-kien", Count = 6 }
            };

            return vm;
        }

        private ProductDetailViewModel GetFallbackProductDetail(int id)
        {
            return new ProductDetailViewModel
            {
                Id = id,
                Name = "Giày Nike Air Jordan 1 Retro High OG",
                BrandName = "Nike",
                CategoryName = "Giày",
                TargetAudience = "Nam",
                ProductType = "Sneakers bóng rổ",
                Price = 3990000,
                OldPrice = 4590000,
                Rating = 4.9,
                ReviewCount = 18,
                Stock = 6,
                Badge = "new",
                ImageUrl = "~/assets/img/Untitled - June 20, 2026 at 02.06.14 (4) (16).png",
                Description = "Thiết kế cổ cao, đệm êm và phối màu dễ mặc cho cả đi học, đi chơi hoặc phối streetwear.",
                Colors = new List<ProductColorViewModel>
                {
                    new ProductColorViewModel { ColorName = "Hồng phấn", HexCode = "#E6CFD1", ImageUrl = "~/assets/img/Untitled - June 20, 2026 at 02.06.14 (4) (16).png" },
                    new ProductColorViewModel { ColorName = "Olive Green", HexCode = "#767B66", ImageUrl = "~/assets/img/BLAZER+MID+'77+OD+NBY (3).avif" },
                    new ProductColorViewModel { ColorName = "Xám", HexCode = "#9ca3af", ImageUrl = "~/assets/img/BLAZER+MID+'77+OD+NBY (2).avif" }
                },
                Sizes = new List<string> { "39", "40", "41", "42", "43" },
                RelatedProducts = new List<ProductItemViewModel>
                {
                    new ProductItemViewModel { Id = 2, Name = "Giày Adidas Ultraboost 2026", BrandName = "Adidas", CategoryName = "Giày", Price = 4290000, ImageUrl = "~/assets/img/image (5).png" },
                    new ProductItemViewModel { Id = 3, Name = "Chuck Taylor All Star", BrandName = "Converse", CategoryName = "Giày", Price = 1700000, ImageUrl = "~/assets/img/image (4).png" }
                }
            };
        }
    }
}
