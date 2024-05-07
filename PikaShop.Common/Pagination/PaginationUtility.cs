
namespace PikaShop.Common.Pagination
{
    public static class PaginationUtility
    {
        public static PaginatedList<T>? ToPaginatedList<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize) where T : class
        {
            var totalCount = query.Count();

            var entities = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return new PaginatedList<T>([.. entities],
                                   totalCount,
                                   pageNumber,
                                   pageSize);
        }

        public static PaginatedList<T>? ToPaginatedList<T>(
            this IOrderedQueryable<T> query,
            int pageNumber,
            int pageSize) where T : class
        {
            // query.OrderBy(...).ToPaginatedList(...)
            var totalCount = query.Count();

            var entities = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return new PaginatedList<T>([.. entities],
                                   totalCount,
                                   pageNumber,
                                   pageSize);
        }

        //public static PaginatedList<T>? ToPaginatedList<T>(
        //    this IOrderedEnumerable<T> query,
        //    int count,
        //    int pageNumber,
        //    int pageSize) where T : class
        //{
            // query.ToPaginatedList(...).OrderBy(...).ToPaginatedList(...)

        //    var entities = query;

        //    return new PaginatedList<T>(entities.ToList(),
        //                           count,
        //                           pageNumber,
        //                           pageSize);
        //}
    }
}
