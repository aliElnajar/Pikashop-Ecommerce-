using PikaShop.Data.Contracts.UnitsOfWork;

namespace PikaShop.Services.Core
{
    public class DepartmentServices(IUnitOfWork _uow) : Contracts.IDepartmentServices
    {
        public IUnitOfWork UnitOfWork { get; set; } = _uow;
    }
}