using Microsoft.EntityFrameworkCore;


namespace Companis.Shared.Requests;

public static class QuerableExtension
{
    public static async Task<PagedList<T>> ToPagedList<T>(
            IQueryable<T> source, int pageNumber, int pageSize)
    {
        //ToDo: Not handled in ExceptionHandler middleware yet... Should return BadRequest and ProblemDetails
        if (pageNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0.");

        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

        var count = await source.CountAsync();

        var items = await source.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}



