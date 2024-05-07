
using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Services.Contracts;

namespace PikaShop.Services.Core
{
	public class OrderServices(IUnitOfWork unitOfWork) : IOrderServices
	{
		public IUnitOfWork UnitOfWork {  get; set; } = unitOfWork;
	}
}
