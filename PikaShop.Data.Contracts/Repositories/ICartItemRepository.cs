using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Data.Contracts.Repositories
{
    public interface ICartItemRepository :
       IRepository<CartItemEntity, int>{
        public void deletebyid(int id, int id2);
          //  void ClearCartItemsForUser(string userId);
    }

}
