using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities;

public class EventPackageEntity
{
    [Key, Column(TypeName = "nvarchar(36)")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [ForeignKey(nameof(Event)), Column(TypeName = "nvarchar(36)")]
    public string EventId { get; set; } = null!;
    public EventEntity Event { get; set; } = null!;

    [ForeignKey(nameof(Package)), Column(TypeName = "nvarchar(36)")]
    public string PackageId { get; set; } = null!;
    public PackageEntity Package { get; set; } = null!;
}