using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Data.Contracts.Partial
{
    public interface ISoftDelete<TEntity,TKey> where TEntity : class
    {
        void SoftDeleteById(TKey id, string username = "system");
        void SoftDelete(TEntity entity, string username = "system");
    }
}
