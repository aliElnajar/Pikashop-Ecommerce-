using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Services.Contracts;

namespace PikaShop.Services.Core
{
    public class CategoryServices(IUnitOfWork _uow) : ICategoryServices
    {
        public IUnitOfWork UnitOfWork { get; set; } = _uow;
    }
}
