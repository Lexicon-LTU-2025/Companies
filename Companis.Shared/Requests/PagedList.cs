using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Companis.Shared.Requests;


public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public MetaData MetaData { get; }

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
            (
                currentPage: pageNumber,
                totalPages: (int)Math.Ceiling(count / (double)pageSize),
                pageSize: pageSize,
                totalCount: count
            );

        Items = items;
    }
}



