using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Exceptions;
public class NotFoundException : Exception
{
    public string Title { get; }
    public NotFoundException(string message, string title = "Not Found") : base(message)
    {
        Title = title;
    }
}
