
namespace PikaShop.Data.Contracts
{
    public interface IEntitySoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
