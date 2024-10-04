using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPedidos.Application.Dtos;

public class PagedFilteredAndSortedInput
{
    public int SkipCount { get; set; }
    public int MaxResultCount { get; set; }
    public string? Sorting { get; set; }
    public string? Filter { get; set; }

    public PagedFilteredAndSortedInput()
    {
        SkipCount = 0;
        MaxResultCount = 25;
    }
}
