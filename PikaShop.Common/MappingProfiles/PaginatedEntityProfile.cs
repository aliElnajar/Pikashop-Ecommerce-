using AutoMapper;
using PikaShop.Common.Pagination;

namespace PikaShop.Common.MappingProfiles
{
    public class PaginatedEntityProfile : Profile

    {
        public PaginatedEntityProfile()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PaginatedListConverter<,>));
        }
    }
}
