using System;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Persistence.UnitsOfWork
{
    public static class UnitOfWorkExtensions
    {
        public static void EnsureSeedDataForContext(this IUnitOfWork unitOfWork)
        {
            if (!unitOfWork.Departments.GetAll().Any())
            {
                // Add Departments Here
                var departments = new List<DepartmentEntity>
                {
                    new() { Name="Electronics", Description="Every Home Device, Mobiles, tablets, and Computers" },
                    new() { Name="Entertainment", Description="" },
                    new() { Name="Books", Description="" },
                    new() { Name="Cloths", Description="" },
                    new() { Name="Tools", Description = "" },
                    new() { Name="Toys", Description = "" },
                    new() { Name = "Groceries", Description = "" },
                    new() { Name="Heavy Machines", Description = "" },
                    new() { Name="Health & Household Department", Description = "" }
                };
                unitOfWork.Departments.CreateRange(departments);
                unitOfWork.Save();
            }

            if (!unitOfWork.Categories.GetAll().Any())
            {
                // Example for Data Seeding
                // DO NOT FORGET to Link the categories to departments

                #region Categories Seeding Example
                var categories = new List<CategoryEntity>
                {
                    new CategoryEntity {Name="Laptop", Description = "", DepartmentID = 1},
                    new CategoryEntity {Name="TV & Audio", Description = "", DepartmentID = 1},
                    new CategoryEntity {Name="Phones", Description = "", DepartmentID = 1},
					new CategoryEntity {Name="Men Fashion", Description = "", DepartmentID = 4},
					new CategoryEntity {Name="Women Fashion", Description = "", DepartmentID = 4},
					new CategoryEntity {Name="Toys", Description = "", DepartmentID = 6}
				};
                unitOfWork.Categories.CreateRange(categories);
                unitOfWork.Save();
                #endregion
            }
            static List<ProductEntity> GenerateSampleProducts(List<CategoryEntity> categories, int count)
            {
                var products = new List<ProductEntity>();
                Random random = new Random();
                for (int i = 0; i < count; i++)
                {
                    var category = categories[random.Next(categories.Count)];

                    products.Add(new ProductEntity
                    {
                        Name = $"Product {i + 1}",
                        Description = $"Description for Product {i + 1}",
                        Price = random.Next(10, 500),
                        UnitsInStock = random.Next(1, 100),
                        CategoryID = category.ID
                    });
                }

                return products;
            }
            if (!unitOfWork.Products.GetAll().Any())
            {
                // Generate 100 sample products
                //var products = GenerateSampleProducts(unitOfWork.Categories.GetAll().ToList(), 100);
                //unitOfWork.Products.CreateRange(products);
                //unitOfWork.Save();

                // >>>>Can NOT Add Products without Categories first!!!!!!<<<<<

                List<ProductEntity> products =
                    [

                    new ProductEntity { Name = "HP Laptop 15t-dy200", Description = "15.6-Inch Laptop with Intel Core i5 processor", Price = 549.99, UnitsInStock = 25, CategoryID = 1, Img = "https://m.media-amazon.com/images/I/713W-C8KlxL._AC_SL1500_.jpg" },
                new ProductEntity { Name = "Dell Inspiron 15 3000", Description = "15.6-Inch Laptop with AMD Ryzen 5 processor", Price = 499.99, UnitsInStock = 30, CategoryID = 1, Img = "https://m.media-amazon.com/images/I/61zRDADh+YS._AC_SL1500_.jpg" },
                new ProductEntity { Name = "Acer Aspire 5", Description = "15.6-Inch Laptop with Intel Core i7 processor", Price = 679.99, UnitsInStock = 35, CategoryID = 1, Img = "https://m.media-amazon.com/images/I/71+2H96GHZL._AC_SL1500_.jpg" },
                new ProductEntity { Name = "Dell Inspiron 15 3000", Description = "Budget-friendly laptop with reliable performance", Price = 499.99, UnitsInStock = 50, CategoryID = 1, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ6u8mjOnGO59CcLk28ovK0kqaHAVbHvAg9l7GGPIUPpw&s" },
                new ProductEntity { Name = "HP Pavilion x360", Description = "Versatile 2-in-1 laptop with touchscreen display", Price = 649.99, UnitsInStock = 45, CategoryID = 1, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuh82jn5j7gCzhGFZ1EYnAH4FpSHADPJBcfA7JOW07VA&s" },

                new ProductEntity { Name = "HP Laptop d439", Description = "HP 14-Inch Laptop", Price = 699.99, UnitsInStock = 30, CategoryID = 1, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0qbvPjLmCwxz7kSxY460bbrA7-oDVvTH009Rbh7OknA&s" },
                new ProductEntity { Name = "HP Omen 17", Description = "HP Omen 17-Inch Gaming Laptop", Price = 1299.99, UnitsInStock = 20, CategoryID = 1, Img = "https://ssl-product-images.www8-hp.com/digmedialib/prodimg/lowres/c05578812.png" },
                new ProductEntity { Name = "Alienware 18", Description = "Alienware 18-Inch Gaming Laptop", Price = 1899.99, UnitsInStock = 25, CategoryID = 1 ,Img = "https://m.media-amazon.com/images/I/71y7IpC-hQL._AC_UF1000,1000_QL80_.jpg"},
                new ProductEntity { Name = "Dell XPS 15", Description = "Dell XPS 15-Inch Laptop", Price = 1399.99, UnitsInStock = 15, CategoryID = 1 , Img = "https://m.media-amazon.com/images/I/91WgL3IbNIL._AC_UF1000,1000_QL80_.jpg"},
                new ProductEntity { Name = "Lenovo ThinkPad X1 Carbon", Description = "Lenovo ThinkPad X1 Carbon 14-Inch Laptop", Price = 1499.99, UnitsInStock = 10, CategoryID = 1 , Img = "https://p4-ofp.static.pub/fes/cms/2023/02/10/7qjkk7h1a53t8jq5snivyzumxw040v193587.png"},

                new ProductEntity { Name = "Lenovo IdeaPad 3", Description = "Affordable laptop with decent performance for everyday tasks", Price = 399.99, UnitsInStock = 40, CategoryID = 1, Img = "https://p1-ofp.static.pub/fes/cms/2022/12/09/wl57o6uv41lwkq1zah6pkt22ihvrp8950736.png" },
                new ProductEntity { Name = "Acer Aspire 5", Description = "Slim and lightweight laptop with good battery life", Price = 549.99, UnitsInStock = 35, CategoryID = 1, Img = "https://i5.walmartimages.com/asr/c72e012b-1ee3-4f63-b198-cd1a9ba09b37.ffa3ec29c734401ef9b7cd3b88a37aff.png" },
                new ProductEntity { Name = "ASUS VivoBook 15", Description = "Stylish laptop with narrow bezels and ergonomic keyboard", Price = 599.99, UnitsInStock = 30, CategoryID = 1, Img = "https://m.media-amazon.com/images/I/810JYMXVRuL._AC_SL1500_.jpg" },
                new ProductEntity { Name = "Apple MacBook Air", Description = "Ultra-thin and lightweight laptop with Retina display", Price = 999.99, UnitsInStock = 25, CategoryID = 1, Img = "https://btech.com/media/catalog/product/cache/4709f4e5925590e2003d78a7a1e77edb/c/3/c35d5b50795ac9827c21fc22575ef232d902f794664497ae32123288c55a0628.jpeg" },
                new ProductEntity { Name = "Microsoft Surface Laptop Go", Description = "Compact and portable laptop with touchscreen display", Price = 699.99, UnitsInStock = 20, CategoryID = 1, Img = "https://m.media-amazon.com/images/I/71OExaOAkbL._AC_SL1500_.jpg" },

                new ProductEntity { Name = "Razer Blade 15", Description = "High-performance gaming laptop with advanced graphics", Price = 1799.99, UnitsInStock = 15, CategoryID = 1, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQRzT0W3eqMa1CpmaoNuVNWY87nIjCklfLWEM_dnEGxww&s" },
                new ProductEntity { Name = "MSI GS66 Stealth", Description = "Powerful gaming laptop with sleek design and RGB keyboard", Price = 1999.99, UnitsInStock = 10, CategoryID = 1, Img = "https://m.media-amazon.com/images/I/41K+mF3eZwS.jpg" },
                new ProductEntity { Name = "Alienware m15 R6", Description = "Premium gaming laptop with customizable lighting and graphics", Price = 2299.99, UnitsInStock = 5, CategoryID = 1, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVmGlhXb8xj-Hp4s1RGoYShVK2pbtr9CadhzrqLeAN8g&s" },
                new ProductEntity { Name = "HP Spectre x360 14", Description = "Luxurious 2-in-1 laptop with OLED display and premium design", Price = 1399.99, UnitsInStock = 50, CategoryID = 1, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQcGga9U-di_04QDdKHnYKsVV1Mn-F9DqLc-aZG5f8I9w&s" },
                new ProductEntity { Name = "Dell XPS 13", Description = "Compact and powerful laptop with InfinityEdge display", Price = 1199.99, UnitsInStock = 45, CategoryID = 1, Img = "https://m.media-amazon.com/images/I/719tCHXNiXL._AC_UF1000,1000_QL80_.jpg" },




                new ProductEntity { Name = "Sharp 4K TV", Description = "Ultra HD 4K Smart TV", Price = 599.99, UnitsInStock = 50, CategoryID = 2 ,Img = "https://m.media-amazon.com/images/S/aplus-media/sota/e2cdf015-53f8-469b-a021-b89a49db9e2e.__CR0,0,970,600_PT0_SX970_V1___.jpg"},
                new ProductEntity { Name = "Samsung Quantum Dot OLED", Description = "Quantum Dot OLED Smart TV", Price = 799.99, UnitsInStock = 45, CategoryID = 2 , Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxwd4wg7TqeIsUUZ9S7u6JIktB-wlmnRn5R4wkIS6Y2Q&s"},
                new ProductEntity { Name = "Sony 8K TV", Description = "8K HDR Smart TV", Price = 1999.99, UnitsInStock = 20, CategoryID = 2 , Img ="https://www.trutone.ca/files/image/attachment/26947/85z9g.jpg"},
                new ProductEntity { Name = "LG NanoCell TV", Description = "NanoCell 4K HDR Smart TV", Price = 899.99, UnitsInStock = 35, CategoryID = 2 ,Img = "https://www.lg.com/ae_ar/images/tvs/md06143396/gallery/49SM8100PVA_D1_V2.jpg"},
                new ProductEntity { Name = "Toshiba Fire TV", Description = "Fire TV Edition Smart TV", Price = 499.99, UnitsInStock = 40, CategoryID = 2 ,Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVmegV3e4DEWdbEoXJ-eRskXMFAUbgzF5OuAnZur_5Tw&s"},


                new ProductEntity { Name = "Samsung 32-Inch LED TV", Description = "Full HD LED TV with vibrant colors", Price = 249.99, UnitsInStock = 50, CategoryID = 2, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxwd4wg7TqeIsUUZ9S7u6JIktB-wlmnRn5R4wkIS6Y2Q&s" },
                new ProductEntity { Name = "LG 43-Inch 4K Smart TV", Description = "Ultra HD Smart TV with webOS", Price = 399.99, UnitsInStock = 45, CategoryID = 2, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxwd4wg7TqeIsUUZ9S7u6JIktB-wlmnRn5R4wkIS6Y2Q&s" },
                new ProductEntity { Name = "Sony 55-Inch OLED TV", Description = "4K HDR OLED TV with Acoustic Surface Audio", Price = 1499.99, UnitsInStock = 20, CategoryID = 2, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVmegV3e4DEWdbEoXJ-eRskXMFAUbgzF5OuAnZur_5Tw&s" },
                new ProductEntity { Name = "TCL 65-Inch QLED TV", Description = "4K QLED TV with Dolby Vision", Price = 799.99, UnitsInStock = 35, CategoryID = 2, Img = "https://www.tcl.com/usca/content/dam/tcl/product/home-theater/6-series/carousel/R625_Front-QLED_CNET_0.png" },
                new ProductEntity { Name = "Vizio 70-Inch LED TV", Description = "Full Array LED TV with SmartCast", Price = 899.99, UnitsInStock = 40, CategoryID = 2, Img = "https://pisces.bbystatic.com/image2/BestBuy_US/images/products/6259/6259880_sd.jpg" },


                new ProductEntity { Name = "Hisense 50-Inch 4K UHD TV", Description = "4K UHD TV with HDR and Android TV", Price = 349.99, UnitsInStock = 30, CategoryID = 2, Img = "https://www.ubuy.com.eg/productimg/?image=aHR0cHM6Ly9pNS53YWxtYXJ0aW1hZ2VzLmNvbS9zZW8vSGlzZW5zZS01MC1DbGFzcy1VTEVELVU2SC1TZXJpZXMtUXVhbnR1bS1Eb3QtUUxFRC00Sy1VSEQtU21hcnQtR29vZ2xlLVRWLTUwVTZIX2I5YmM2YzdjLTc0MmItNDA3Yy04Y2E0LWMxODA1MmU0YWIyNC5mMmJmOTI3Y2ZjMzZhMmZhMTEyOWIyYTkyYjg4NTNhYy5wbmc.jpg" },
                new ProductEntity { Name = "Samsung 75-Inch Neo QLED TV", Description = "8K Neo QLED TV with Quantum HDR 64X", Price = 3999.99, UnitsInStock = 25, CategoryID = 2, Img = "https://images.samsung.com/is/image/samsung/p6pim/in/qa75qn90cakxxl/gallery/in-qled-qn90c-454657-qa75qn90cakxxl-536415727?$720_576_PNG$" },
                new ProductEntity { Name = "LG 55-Inch NanoCell TV", Description = "4K NanoCell TV with AI ThinQ", Price = 799.99, UnitsInStock = 30, CategoryID = 2, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSXth8b8kYhH_VZgOZcrK7jtic3jyTcfhB2iNGnoa5HjA&s" },
                new ProductEntity { Name = "Sony 65-Inch 8K HDR TV", Description = "8K HDR TV with Full Array LED and X1 Ultimate processor", Price = 2999.99, UnitsInStock = 15, CategoryID = 2, Img = "https://s.yimg.com/ny/api/res/1.2/HODbjlldOOeCHco_UeI2Nw--/YXBwaWQ9aGlnaGxhbmRlcjt3PTY0MDtoPTQyNw--/https://o.aolcdn.com/images/dims?crop=1600%2C1067%2C0%2C0&quality=85&format=jpg&resize=1600%2C1067&image_uri=https://s.yimg.com/os/creatr-uploaded-images/2020-01/506bbdf0-30ba-11ea-b6d6-162ad1e97745&client=a1acac3e1b3290917d92&signature=1bd9e3238c9d26b02126c5a0108e70a9fa1fd1ab" },
                new ProductEntity { Name = "Toshiba 43-Inch Fire TV", Description = "Full HD Smart TV with Fire TV experience", Price = 279.99, UnitsInStock = 20, CategoryID = 2, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQAdPQ04RIOYXha1oCyLY7irNsRyGTb60PFzeXcts9qIQ&s" },







                new ProductEntity { Name = "Apple iPhone 13", Description = "Latest iPhone model with advanced features", Price = 999.99, UnitsInStock = 50, CategoryID = 3, Img = "https://etisal-storeapi.witheldokan.com/storage/uploads/iphone-13-blue-select-2021_1_1_1.png" },
                new ProductEntity { Name = "Samsung Galaxy S22 Ultra", Description = "Flagship Samsung phone with top-of-the-line specs", Price = 1199.99, UnitsInStock = 45, CategoryID = 3, Img = "https://i5.walmartimages.com/seo/Samsung-Galaxy-S22-ULTRA-5G-128GB-GREEN-Unlocked_74f7eb8f-8c14-4192-bff8-1bd1e91a3b97.f4f2dc71a43be3abebbcfd157bc81500.jpeg" },
                new ProductEntity { Name = "Google Pixel 6 Pro", Description = "High-end Google Pixel phone with advanced camera features", Price = 899.99, UnitsInStock = 40, CategoryID = 3, Img = "https://m.media-amazon.com/images/I/71SGl7xwR-L._AC_SL1500_.jpg" },
                new ProductEntity { Name = "OnePlus 10 Pro", Description = "OnePlus flagship phone with high performance and sleek design", Price = 899.99, UnitsInStock = 35, CategoryID = 3, Img = "https://www.ubuy.com.eg/productimg/?image=aHR0cHM6Ly9tLm1lZGlhLWFtYXpvbi5jb20vaW1hZ2VzL0kvODFxT0Q4RVpUakwuX0FDX1NMMTUwMF8uanBn.jpg" },
                new ProductEntity { Name = "Xiaomi Mi 12 Ultra", Description = "Xiaomi flagship phone with cutting-edge technology", Price = 1099.99, UnitsInStock = 30, CategoryID = 3, Img = "https://m.media-amazon.com/images/I/71D5CY2j3YL._AC_UF1000,1000_QL80_.jpg" },



                new ProductEntity { Name = "Sony Xperia 1 III", Description = "Sony flagship phone with focus on photography and multimedia", Price = 1099.99, UnitsInStock = 25, CategoryID = 3, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSO6rXJLzXeWS9pVYGUmZwvYq4WMSMuu4mlp9sZ4YvKLw&s" },
                new ProductEntity { Name = "Huawei P50 Pro", Description = "Huawei flagship phone with innovative camera technology", Price = 999.99, UnitsInStock = 20, CategoryID = 3, Img = "https://www.ubuy.com.eg/productimg/?image=aHR0cHM6Ly9tLm1lZGlhLWFtYXpvbi5jb20vaW1hZ2VzL0kvODFxT0Q4RVpUakwuX0FDX1NMMTUwMF8uanBn.jpg" },
                new ProductEntity { Name = "Oppo Find X5 Pro", Description = "Oppo flagship phone with sleek design and powerful performance", Price = 1099.99, UnitsInStock = 15, CategoryID = 3, Img = "https://i5.walmartimages.com/seo/Samsung-Galaxy-S22-ULTRA-5G-128GB-GREEN-Unlocked_74f7eb8f-8c14-4192-bff8-1bd1e91a3b97.f4f2dc71a43be3abebbcfd157bc81500.jpeg" },
                new ProductEntity { Name = "Vivo X80 Pro+", Description = "Vivo flagship phone with high-end features and performance", Price = 999.99, UnitsInStock = 10, CategoryID = 3, Img = "https://m.media-amazon.com/images/I/71SGl7xwR-L._AC_SL1500_.jpg" },
                new ProductEntity { Name = "Realme GT 2 Pro", Description = "Realme flagship phone with premium design and specifications", Price = 899.99, UnitsInStock = 5, CategoryID = 3, Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSvQxatnLSB5uBYjnUxRdmYO1KAONNdQhiKklDg3LQLSQ&s" },

                new ProductEntity { Name = "Apple iPhone 14", Description = "Latest iPhone model with advanced features", Price = 1999.99, UnitsInStock = 50, CategoryID = 3, Img = "https://etisal-storeapi.witheldokan.com/storage/uploads/iphone-13-blue-select-2021_1_1_1.png" },
                new ProductEntity { Name = "Samsung Galaxy S23 Ultra", Description = "Flagship Samsung phone with top-of-the-line specs", Price = 1299.99, UnitsInStock = 45, CategoryID = 3, Img = "https://i5.walmartimages.com/seo/Samsung-Galaxy-S22-ULTRA-5G-128GB-GREEN-Unlocked_74f7eb8f-8c14-4192-bff8-1bd1e91a3b97.f4f2dc71a43be3abebbcfd157bc81500.jpeg" },
                new ProductEntity { Name = "Google Pixel 7 Pro", Description = "High-end Google Pixel phone with advanced camera features", Price = 299.99, UnitsInStock = 40, CategoryID = 3, Img = "https://m.media-amazon.com/images/I/71SGl7xwR-L._AC_SL1500_.jpg" },
                new ProductEntity { Name = "OnePlus 11 Pro", Description = "OnePlus flagship phone with high performance and sleek design", Price = 999.99, UnitsInStock = 35, CategoryID = 3, Img = "https://www.ubuy.com.eg/productimg/?image=aHR0cHM6Ly9tLm1lZGlhLWFtYXpvbi5jb20vaW1hZ2VzL0kvODFxT0Q4RVpUakwuX0FDX1NMMTUwMF8uanBn.jpg" },
                new ProductEntity { Name = "Xiaomi Mi 11 Ultra", Description = "Xiaomi flagship phone with cutting-edge technology", Price = 1699.99, UnitsInStock = 30, CategoryID = 3, Img = "https://m.media-amazon.com/images/I/71D5CY2j3YL._AC_UF1000,1000_QL80_.jpg" },



                new ProductEntity { Name = "Sony Xperia 2 III", Description = "Sony flagship phone with focus on photography and multimedia", Price = 1599.99, UnitsInStock = 25, CategoryID = 3, Img = "https://m.media-amazon.com/images/I/51TlkYkvWxS._AC_UF894,1000_QL80_.jpg" },
                new ProductEntity { Name = "Huawei P60 Pro", Description = "Huawei flagship phone with innovative camera technology", Price = 3499.99, UnitsInStock = 20, CategoryID = 3, Img = "https://www.ubuy.com.eg/productimg/?image=aHR0cHM6Ly9tLm1lZGlhLWFtYXpvbi5jb20vaW1hZ2VzL0kvODFxT0Q4RVpUakwuX0FDX1NMMTUwMF8uanBn.jpg" },
                new ProductEntity { Name = "Oppo Find X6 Pro", Description = "Oppo flagship phone with sleek design and powerful performance", Price = 1099.99, UnitsInStock = 15, CategoryID = 3, Img = "https://i5.walmartimages.com/seo/Samsung-Galaxy-S22-ULTRA-5G-128GB-GREEN-Unlocked_74f7eb8f-8c14-4192-bff8-1bd1e91a3b97.f4f2dc71a43be3abebbcfd157bc81500.jpeg" },
                new ProductEntity { Name = "Vivo X70 Pro+", Description = "Vivo flagship phone with high-end features and performance", Price = 1999.99, UnitsInStock = 10, CategoryID = 3, Img = "https://m.media-amazon.com/images/I/71SGl7xwR-L._AC_SL1500_.jpg" },
                new ProductEntity { Name = "Realme GT 3 Pro", Description = "Realme flagship phone with premium design and specifications", Price = 3899.99, UnitsInStock = 5, CategoryID = 3, Img = "https://cdn1.smartprix.com/rx-i2H0SF0Px-w1200-h1200/2H0SF0Px.jpg" },

];
                unitOfWork.Products.CreateRange(products);
                unitOfWork.Save();
            }

            if (!unitOfWork.CategorySpecs.GetAll().Any())
            {


                List<CategorySpecsEntity> categorySpecs = [
                    new CategorySpecsEntity { CategoryID = 1, Key = "Brand", Value = "" },
                    new CategorySpecsEntity { CategoryID = 1, Key = "Ram Size", Value = "" },

                    new CategorySpecsEntity { CategoryID = 2, Key = "Brand", Value = "" },
                    new CategorySpecsEntity { CategoryID = 2, Key = "Manufacturer", Value = "" },
                    new CategorySpecsEntity { CategoryID = 2, Key = "Screen Size", Value = "" },

                    new CategorySpecsEntity { CategoryID = 3, Key = "Brand", Value = "" },
                    new CategorySpecsEntity { CategoryID = 3, Key = "Color", Value = "" },
                    new CategorySpecsEntity { CategoryID = 3, Key = "Manufacturer", Value = "" },
                    new CategorySpecsEntity { CategoryID = 3, Key = "Screen Size", Value = "" }

                    ];

                unitOfWork.CategorySpecs.CreateRange(categorySpecs);
                unitOfWork.Save();

            }


            if (!unitOfWork.ProductSpecs.GetAll().Any())
            {


                List<ProductSpecsEntity> productSpecs = [

                    new ProductSpecsEntity { ProductID = 1, Key = "Brand", Value = "HP" },
                    new ProductSpecsEntity { ProductID = 1, Key = "Ram Size", Value = "8GB" },



                    new ProductSpecsEntity { ProductID = 3, Key = "Brand", Value = "Acer" },
                    new ProductSpecsEntity { ProductID = 3, Key = "Ram Size", Value = "16GB" },

                    new ProductSpecsEntity { ProductID = 6, Key = "Ram Size", Value = "12GB" },


                    new ProductSpecsEntity { ProductID = 7, Key = "Ram Size", Value = "16GB" },

                    new ProductSpecsEntity { ProductID = 8, Key = "Brand", Value = "Alienware" },
                    new ProductSpecsEntity { ProductID = 8, Key = "Ram Size", Value = "32GB" },

                    new ProductSpecsEntity { ProductID = 9, Key = "Ram Size", Value = "64GB" },

                    new ProductSpecsEntity { ProductID = 10, Key = "Brand", Value = "Lenovo" },
                    new ProductSpecsEntity { ProductID = 10, Key = "Ram Size", Value = "128GB" },

                    new ProductSpecsEntity { ProductID = 11, Key = "Brand", Value = "Lenovo" },


                    new ProductSpecsEntity { ProductID = 12, Key = "Brand", Value = "Acer" },


                    new ProductSpecsEntity { ProductID = 13, Key = "Brand", Value = "ASUS" },


                    new ProductSpecsEntity { ProductID = 14, Key = "Brand", Value = "Apple" },


                    new ProductSpecsEntity { ProductID = 15, Key = "Brand", Value = "Microsoft" },


                    new ProductSpecsEntity { ProductID = 16, Key = "Brand", Value = "Razer" },


                    new ProductSpecsEntity { ProductID = 17, Key = "Brand", Value = "MSI" },


                    new ProductSpecsEntity { ProductID = 18, Key = "Brand", Value = "Alienware" },


                    new ProductSpecsEntity { ProductID = 19, Key = "Brand", Value = "HP" },


                    new ProductSpecsEntity { ProductID = 20, Key = "Brand", Value = "Dell" },



                    new ProductSpecsEntity { ProductID = 21, Key = "Brand", Value = "Sharp" },
                    new ProductSpecsEntity { ProductID = 21, Key = "Manufacturer", Value = "Sharp Electronics" },
                    new ProductSpecsEntity { ProductID = 21, Key = "Screen Size", Value = "65 inches" },

                    new ProductSpecsEntity { ProductID = 22, Key = "Brand", Value = "Samsung" },
                    new ProductSpecsEntity { ProductID = 22, Key = "Manufacturer", Value = "Samsung Electronics" },
                    new ProductSpecsEntity { ProductID = 22, Key = "Screen Size", Value = "55 inches" },

                    new ProductSpecsEntity { ProductID = 23, Key = "Brand", Value = "Sony" },
                    new ProductSpecsEntity { ProductID = 23, Key = "Manufacturer", Value = "Sony Corporation" },
                    new ProductSpecsEntity { ProductID = 23, Key = "Screen Size", Value = "85 inches" },

                    new ProductSpecsEntity { ProductID = 24, Key = "Brand", Value = "LG" },
                    new ProductSpecsEntity { ProductID = 24, Key = "Manufacturer", Value = "LG Electronics" },
                    new ProductSpecsEntity { ProductID = 24, Key = "Screen Size", Value = "75 inches" },

                    new ProductSpecsEntity { ProductID = 25, Key = "Brand", Value = "Toshiba" },
                    new ProductSpecsEntity { ProductID = 25, Key = "Manufacturer", Value = "Toshiba Corporation" },
                    new ProductSpecsEntity { ProductID = 25, Key = "Screen Size", Value = "55 inches" },

                    new ProductSpecsEntity { ProductID = 26, Key = "Brand", Value = "Samsung" },
                    new ProductSpecsEntity { ProductID = 26, Key = "Manufacturer", Value = "Samsung Electronics" },
                    new ProductSpecsEntity { ProductID = 26, Key = "Screen Size", Value = "32 inches" },

                    new ProductSpecsEntity { ProductID = 27, Key = "Brand", Value = "LG" },
                    new ProductSpecsEntity { ProductID = 27, Key = "Manufacturer", Value = "LG Electronics" },
                    new ProductSpecsEntity { ProductID = 27, Key = "Screen Size", Value = "43 inches" },

                    new ProductSpecsEntity { ProductID = 28, Key = "Brand", Value = "Sony" },
                    new ProductSpecsEntity { ProductID = 28, Key = "Manufacturer", Value = "Sony Corporation" },
                    new ProductSpecsEntity { ProductID = 28, Key = "Screen Size", Value = "55 inches" },

                    new ProductSpecsEntity { ProductID = 29, Key = "Brand", Value = "TCL" },
                    new ProductSpecsEntity { ProductID = 29, Key = "Manufacturer", Value = "TCL Corporation" },
                    new ProductSpecsEntity { ProductID = 29, Key = "Screen Size", Value = "65 inches" },

                    new ProductSpecsEntity { ProductID = 30, Key = "Brand", Value = "Vizio" },
                    new ProductSpecsEntity { ProductID = 30, Key = "Manufacturer", Value = "Vizio Inc." },
                    new ProductSpecsEntity { ProductID = 30, Key = "Screen Size", Value = "70 inches" },


                    new ProductSpecsEntity { ProductID = 31, Key = "Brand", Value = "Hisense" },
                    new ProductSpecsEntity { ProductID = 31, Key = "Manufacturer", Value = "Hisense Company Ltd." },
                    new ProductSpecsEntity { ProductID = 31, Key = "Screen Size", Value = "50 inches" },

                    new ProductSpecsEntity { ProductID = 32, Key = "Brand", Value = "Samsung" },
                    new ProductSpecsEntity { ProductID = 32, Key = "Manufacturer", Value = "Samsung Electronics" },
                    new ProductSpecsEntity { ProductID = 32, Key = "Screen Size", Value = "75 inches" },

                    new ProductSpecsEntity { ProductID = 33, Key = "Brand", Value = "LG" },
                    new ProductSpecsEntity { ProductID = 33, Key = "Manufacturer", Value = "LG Electronics" },
                    new ProductSpecsEntity { ProductID = 33, Key = "Screen Size", Value = "55 inches" },

                    new ProductSpecsEntity { ProductID = 34, Key = "Brand", Value = "Sony" },
                    new ProductSpecsEntity { ProductID = 34, Key = "Manufacturer", Value = "Sony Corporation" },
                    new ProductSpecsEntity { ProductID = 34, Key = "Screen Size", Value = "65 inches" },

                    new ProductSpecsEntity { ProductID = 35, Key = "Brand", Value = "Toshiba" },
                    new ProductSpecsEntity { ProductID = 35, Key = "Manufacturer", Value = "Toshiba Corporation" },
                    new ProductSpecsEntity { ProductID = 35, Key = "Screen Size", Value = "43 inches" },

                    new ProductSpecsEntity { ProductID = 36, Key = "Brand", Value = "Apple" },
                    new ProductSpecsEntity { ProductID = 36, Key = "Color", Value = "Blue" },
                    new ProductSpecsEntity { ProductID = 36, Key = "Manufacturer", Value = "Apple Inc." },


                    new ProductSpecsEntity { ProductID = 37, Key = "Brand", Value = "Samsung" },
                    new ProductSpecsEntity { ProductID = 37, Key = "Color", Value = "Green" },
                    new ProductSpecsEntity { ProductID = 37, Key = "Manufacturer", Value = "Samsung Electronics" },


                    new ProductSpecsEntity { ProductID = 38, Key = "Brand", Value = "Google" },
                    new ProductSpecsEntity { ProductID = 38, Key = "Manufacturer", Value = "Google LLC" },


                    new ProductSpecsEntity { ProductID = 39, Key = "Brand", Value = "OnePlus" },
                    new ProductSpecsEntity { ProductID = 39, Key = "Manufacturer", Value = "OnePlus Technology (Shenzhen) Co., Ltd." },


                    new ProductSpecsEntity { ProductID = 40, Key = "Brand", Value = "Xiaomi" },
                    new ProductSpecsEntity { ProductID = 40, Key = "Color", Value = "Not specified" },
                    new ProductSpecsEntity { ProductID = 40, Key = "Manufacturer", Value = "Xiaomi Corporation" },


                    new ProductSpecsEntity { ProductID = 41, Key = "Brand", Value = "Sony" },
                    new ProductSpecsEntity { ProductID = 41, Key = "Manufacturer", Value = "Sony Corporation" },


                    new ProductSpecsEntity { ProductID = 42, Key = "Brand", Value = "Huawei" },
                    new ProductSpecsEntity { ProductID = 42, Key = "Manufacturer", Value = "Huawei Technologies Co., Ltd." },


                    new ProductSpecsEntity { ProductID = 43, Key = "Brand", Value = "Oppo" },
                    new ProductSpecsEntity { ProductID = 43, Key = "Manufacturer", Value = "Guangdong Oppo Mobile Telecommunications Corp., Ltd." },


                    new ProductSpecsEntity { ProductID = 44, Key = "Brand", Value = "Vivo" },
                    new ProductSpecsEntity { ProductID = 44, Key = "Manufacturer", Value = "BBK Electronics Corporation (Vivo)" },


                    new ProductSpecsEntity { ProductID = 45, Key = "Brand", Value = "Realme" },
                    new ProductSpecsEntity { ProductID = 45, Key = "Manufacturer", Value = "Realme Mobile Telecommunications (India) Private Limited" },


                    new ProductSpecsEntity { ProductID = 46, Key = "Brand", Value = "Apple" },
                    new ProductSpecsEntity { ProductID = 46, Key = "Manufacturer", Value = "Apple Inc." },


                    new ProductSpecsEntity { ProductID = 47, Key = "Brand", Value = "Samsung" },
                    new ProductSpecsEntity { ProductID = 47, Key = "Manufacturer", Value = "Samsung Electronics Co., Ltd." },


                    new ProductSpecsEntity { ProductID = 48, Key = "Brand", Value = "Google" },
                    new ProductSpecsEntity { ProductID = 48, Key = "Manufacturer", Value = "Google LLC" },


                    new ProductSpecsEntity { ProductID = 49, Key = "Brand", Value = "OnePlus" },
                    new ProductSpecsEntity { ProductID = 49, Key = "Manufacturer", Value = "OnePlus Technology (Shenzhen) Co., Ltd." },


                    new ProductSpecsEntity { ProductID = 50, Key = "Brand", Value = "Xiaomi" },
                    new ProductSpecsEntity { ProductID = 50, Key = "Manufacturer", Value = "Xiaomi Corporation" },


                    new ProductSpecsEntity { ProductID = 51, Key = "Brand", Value = "Sony" },
                    new ProductSpecsEntity { ProductID = 51, Key = "Manufacturer", Value = "Sony Corporation" },


                    new ProductSpecsEntity { ProductID = 52, Key = "Brand", Value = "Huawei" },
                    new ProductSpecsEntity { ProductID = 52, Key = "Manufacturer", Value = "Huawei Technologies Co., Ltd." },


                    new ProductSpecsEntity { ProductID = 53, Key = "Brand", Value = "Oppo" },
                    new ProductSpecsEntity { ProductID = 53, Key = "Manufacturer", Value = "Guangdong Oppo Mobile Telecommunications Corp., Ltd." },


                    new ProductSpecsEntity { ProductID = 54, Key = "Brand", Value = "Vivo" },
                    new ProductSpecsEntity { ProductID = 54, Key = "Manufacturer", Value = "Vivo Communication Technology Co. Ltd." },


                    new ProductSpecsEntity { ProductID = 55, Key = "Brand", Value = "Realme" },
                    new ProductSpecsEntity { ProductID = 55, Key = "Manufacturer", Value = "Realme Mobile Telecommunications (India) Private Limited" },


                    ];

                unitOfWork.ProductSpecs.CreateRange(productSpecs);
                unitOfWork.Save();

            }


            if (!unitOfWork.Orders.GetAll().Any())
            {


                List<OrderEntity> orders = [
                    new OrderEntity { CustomerID = 3, Total= 11140 , Status = "Delivered",PaymentMethod = Entities.Enums.PaymentMethods.Stripe , OrderedAt = DateTime.Now, DateCreated = DateTime.Now, DateModified = DateTime.Now,TransactionID = "ASQWE223S2SD212DDD232", Items = new List<OrderItemEntity>(){
                        new OrderItemEntity() { ProductID = 1, OrderID = 1, Quantity = 5, SellingPrice = 550 ,SubTotal = 2750},
                        new OrderItemEntity() { ProductID = 2, OrderID = 1, Quantity = 4, SellingPrice = 500 ,SubTotal = 2000},
                        new OrderItemEntity() { ProductID = 3, OrderID = 1, Quantity = 3, SellingPrice = 680 ,SubTotal = 2040},
                        new OrderItemEntity() { ProductID = 8, OrderID = 1, Quantity = 2, SellingPrice = 1900 ,SubTotal = 3800},
                        new OrderItemEntity() { ProductID = 12, OrderID = 1, Quantity = 1, SellingPrice = 550 ,SubTotal = 550},
                    }   },

                    new OrderEntity { CustomerID = 3, Total= 13950 , Status = "Delivered",PaymentMethod = Entities.Enums.PaymentMethods.Stripe , OrderedAt = DateTime.Now, DateCreated = DateTime.Now, DateModified = DateTime.Now,TransactionID = "123DSD1EDASD12EQWDQWD", Items = new List<OrderItemEntity>(){
                        new OrderItemEntity() { ProductID = 1, OrderID = 2, Quantity = 1, SellingPrice = 550 ,SubTotal = 550},
                        new OrderItemEntity() { ProductID = 7, OrderID = 2, Quantity = 2, SellingPrice = 1300 ,SubTotal = 2600},
                        new OrderItemEntity() { ProductID = 3, OrderID = 2, Quantity = 5, SellingPrice = 680 ,SubTotal = 3400},
                        new OrderItemEntity() { ProductID = 12, OrderID = 2, Quantity = 2, SellingPrice = 550 ,SubTotal = 1100},
                        new OrderItemEntity() { ProductID = 15, OrderID = 2, Quantity = 9, SellingPrice = 700 ,SubTotal = 6300},
                    }   },

                   new OrderEntity { CustomerID = 3, Total= 22000 , Status = "Pending",PaymentMethod = Entities.Enums.PaymentMethods.CashOnDelivery , OrderedAt = DateTime.Now, DateCreated = DateTime.Now, DateModified = DateTime.Now,TransactionID = "ASDWQEGF343D32D3D23D", Items = new List<OrderItemEntity>(){
                        new OrderItemEntity() { ProductID = 9, OrderID = 3, Quantity = 1, SellingPrice = 1400 ,SubTotal = 1400},
                        new OrderItemEntity() { ProductID = 2, OrderID = 3, Quantity = 5, SellingPrice = 500 ,SubTotal = 2500},
                        new OrderItemEntity() { ProductID = 4, OrderID = 3, Quantity = 3, SellingPrice = 500 ,SubTotal = 1500},
                        new OrderItemEntity() { ProductID = 7, OrderID = 3, Quantity = 2, SellingPrice = 1300 ,SubTotal = 2600},
                        new OrderItemEntity() { ProductID = 17, OrderID = 3, Quantity = 7, SellingPrice = 2000 ,SubTotal = 14000},
                    }   }

                    ];


                unitOfWork.Orders.CreateRange(orders);
                unitOfWork.Save();

            }

            if (!unitOfWork.CartItems.GetAll().Any())
            {


                List<CartItemEntity> cart = [
                    new CartItemEntity { CustomerID = 3, ProductID = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now, Quantity = 5},
                    new CartItemEntity { CustomerID = 3, ProductID = 3, DateCreated = DateTime.Now, DateModified = DateTime.Now, Quantity = 2},
                    new CartItemEntity { CustomerID = 3, ProductID = 6, DateCreated = DateTime.Now, DateModified = DateTime.Now, Quantity = 7},
                    new CartItemEntity { CustomerID = 3, ProductID = 16, DateCreated = DateTime.Now, DateModified = DateTime.Now, Quantity = 1}
                ];

                unitOfWork.CartItems.CreateRange(cart);
                unitOfWork.Save();

            }


            if (!unitOfWork.WishList.GetAll().Any())
            {


                List<WishListEntity> wishList = [
                    new WishListEntity { CustomerID = 3, ProductID = 3, DateCreated = DateTime.Now, DateModified = DateTime.Now, Quantity = 12},
                    new WishListEntity { CustomerID = 3, ProductID = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now, Quantity = 2},
                    new WishListEntity { CustomerID = 3, ProductID = 1, DateCreated = DateTime.Now, DateModified = DateTime.Now, Quantity = 2},
                    new WishListEntity { CustomerID = 3, ProductID = 7, DateCreated = DateTime.Now, DateModified = DateTime.Now, Quantity = 4}
                ];

                unitOfWork.WishList.CreateRange(wishList);
                unitOfWork.Save();

            }

        }
    }
}