using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Repositories;

namespace PikaShop.Data.Persistence.Repositories
{
    public class DepartmentRepository(ApplicationDbContext _context) : Repository<DepartmentEntity, int>(_context), IDepartmentRepository
    {
        public override IQueryable<DepartmentEntity> GetAll()
        {
            return context.Departments.Where(d=> !d.IsDeleted);
        }

        public void UpdateById(int id, DepartmentEntity other, string username = "system")
        {
            DepartmentEntity? oldDep = GetById(id);
            if(oldDep != null)
            {
               oldDep.Description = other.Description;
               oldDep.Name = other.Name;
                UpdateAudit(oldDep,username);
            }
        }
        public void Update(DepartmentEntity entity, DepartmentEntity other, string username = "system")
        {
            UpdateById(entity.ID, other);
        }
    }
}
