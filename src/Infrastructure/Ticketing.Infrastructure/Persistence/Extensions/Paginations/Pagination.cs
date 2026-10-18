using System.ComponentModel.DataAnnotations;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Infrastructure.Persistence.Extensions.Paginations;

public class Pagination : IPagination
{

    [Range(0, int.MaxValue)]
    public int? offset { get; set; }

    [Range(0, int.MaxValue)]
    public int? Limit { get; set; }
}
