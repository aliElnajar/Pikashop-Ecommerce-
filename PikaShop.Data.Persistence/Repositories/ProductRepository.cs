using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Repositories;

namespace PikaShop.Data.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext context) :  Repository<ProductEntity, int>(context), IProductRepository
    {
        public void UpdateById(int id, ProductEntity other, string username = "system")
        {
            ProductEntity? editedProduct = GetById(id);
            if (editedProduct != null)
            {
                editedProduct.Description = other.Description;
                editedProduct.Price = other.Price;
                editedProduct.Name = other.Name;
                editedProduct.Img=other.Img;
                editedProduct.UnitsInStock = other.UnitsInStock;
                editedProduct.CategoryID = other.CategoryID;
                editedProduct.Category = other.Category;
                UpdateAudit(editedProduct,username);
            }

        }
        public void Update(ProductEntity entity, ProductEntity other, string username = "system")
        {
            UpdateById(entity.ID, other);

        }

        public void Update(ProductEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
