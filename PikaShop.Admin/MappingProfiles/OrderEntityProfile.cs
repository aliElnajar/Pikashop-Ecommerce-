using AutoMapper;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Admin.MappingProfiles
{
	public class OrderEntityProfile : Profile
	{
		public OrderEntityProfile()
		{
			CreateMap<OrderEntity, OrderViewModel>().ReverseMap();
		}
	}
}
