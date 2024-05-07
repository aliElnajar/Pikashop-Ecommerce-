using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Services.Contracts;

namespace PikaShop.Services.Core
{
    public class CartItemServices(IUnitOfWork _uow) : Contracts.ICartItemServices
    {
       
        public IUnitOfWork UnitOfWork { get; set; } = _uow;
   
        public void DeleteCartItem(int id, int id1)
        {
           
        }
   
    }
    
}
