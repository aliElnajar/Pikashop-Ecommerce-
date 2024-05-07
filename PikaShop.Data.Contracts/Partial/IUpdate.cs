
namespace PikaShop.Data.Contracts.Partial
{
    public interface IUpdate<TEntity, TKey>
    {
        public void Update(TEntity entity, TEntity other, string username = "system");

        public void UpdateById(TKey key, TEntity other,string username="system");
    }
}
