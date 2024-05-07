using PikaShop.Data.Context.ContextEntities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Data.Contracts.Repositories
{
    public interface IWishListRepository :
   IRepository<WishListEntity, int>
    {
        public void deletebyid(int id, int id2);
    }
}