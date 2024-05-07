using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Services.Contracts;

namespace PikaShop.Services.Core
{
    public class ReviewServices(IUnitOfWork _uow) : IReviewServices
    {
        public IUnitOfWork UnitOfWork { get; set; } = _uow;
    }
}
