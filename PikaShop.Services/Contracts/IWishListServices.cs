using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Services.Contracts
{
    public interface IWishListServices : IServices
    {

        public void DeleteWishListItem(int id, int id1);

    }
}