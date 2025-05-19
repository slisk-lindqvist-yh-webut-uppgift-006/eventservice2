using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities;

public class PackageEntity
{
    [Key, Column(TypeName = "nvarchar(36)")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column(TypeName = "nvarchar(150)")]
    public string Title { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string SeatingArrangement { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string? Placement { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "nvarchar(10)")]
    public string? Currency { get; set; }

    // public ICollection<EventPackageEntity> Events { get; set; } = [];
}