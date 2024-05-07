using PikaShop.Data.Contracts.Repositories;

namespace PikaShop.Data.Contracts.UnitsOfWork
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IDepartmentRepository Departments { get; }
        IProductRepository Products { get; }
        ICategorySpecsRepository CategorySpecs { get; }
        IProductSpecsRepository ProductSpecs { get; }
        IOrderRepository Orders { get; }

        ICartItemRepository CartItems { get; }

        IOrderItemRepository OrderItems { get; }

        IReviewRepository Reviews { get; }

        IWishListRepository WishList { get; }

        int Save();

        Task<int> SaveAsync();
    }
}
