using PikaShop.Data.Contracts.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Services.Core
{
    public class WishListServices(IUnitOfWork _uow) : Contracts.IWishListServices
    {

        public IUnitOfWork UnitOfWork { get; set; } = _uow;

        public void DeleteWishListItem(int id, int id1)
        {

        }

    }
}