using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Repositories;

namespace PikaShop.Data.Persistence.Repositories
{
    public class CategorySpecsRepository(ApplicationDbContext _context) : Repository<CategorySpecsEntity, int>(_context), ICategorySpecsRepository
    {
        public override IQueryable<CategorySpecsEntity> GetAll()
        {
            return context.CategorySpecTemplates.Where(cs => !cs.IsDeleted).AsNoTracking();
        }
        public IQueryable<CategorySpecsEntity> GetAllByCategoryID(int categoryID)
        {
            return context.CategorySpecTemplates.Where(cs=>cs.CategoryID == categoryID).AsNoTracking();
        }

        public void UpdateById(int id, CategorySpecsEntity other, string username = "system")
        {
            CategorySpecsEntity? oldCategorySpecs = GetById(id);
            if (oldCategorySpecs != null)
            {
                oldCategorySpecs.Key = other.Key;
                oldCategorySpecs.Value = other.Value;
                UpdateAudit(oldCategorySpecs,username);
            }
        }

        public void Update(CategorySpecsEntity entity, CategorySpecsEntity other, string username = "system")
        {
            UpdateById(entity.ID, other);
        }
    }
}
