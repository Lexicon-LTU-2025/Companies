using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companis.Shared.Requests;
public abstract class RequestBaseParameters
{
    private int pageSize = 2;
    const int maxPageSize = 20;
    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => pageSize;
        set => pageSize = value > maxPageSize ? maxPageSize : value;
    }
}
