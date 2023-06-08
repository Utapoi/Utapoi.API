using System.ComponentModel.DataAnnotations;

namespace Karaoke.Application.Common.Requests;

public class PaginatedRequest
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Skip { get; set; } = 0;

    [Required]
    [Range(1, 50)]
    public int Take { get; set; } = 10;
}