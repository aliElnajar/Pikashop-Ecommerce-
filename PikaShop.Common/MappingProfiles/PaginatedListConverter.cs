using AutoMapper;
using PikaShop.Common.Pagination;

namespace PikaShop.Common.MappingProfiles
{
    public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
        where TSource : class
        where TDestination : class
    {
        public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination,
            ResolutionContext context)
        {
            destination ??= [];
            foreach (var item in source)
            {
                var dest = context.Mapper.Map<TSource, TDestination>(item);
                destination.Add(dest);
            }
            destination.PageSize = source.PageSize;
            destination.TotalCount = source.TotalCount;
            destination.CurrentPage = source.CurrentPage;
            destination.TotalPages = source.TotalPages;

            return destination;
        }
    }
}
