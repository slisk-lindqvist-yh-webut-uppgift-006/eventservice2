using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Authentication;

namespace Persistence.Entities;

public class EventEntity
{
    [Key, Column(TypeName = "nvarchar(36)")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column(TypeName = "nvarchar(255)")]
    public string? Image { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string? Title { get; set; }

    [Column(TypeName = "nvarchar(1000)")]
    public string? Description { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime EventDate { get; set; }
    
    // [Column(TypeName = "date")]
    // public DateTime Date { get; set; }
    //
    // [Column(TypeName = "time")]
    // public TimeSpan Time { get; set; }

    [Column(TypeName = "nvarchar(300)")]
    public string? Location { get; set; }
    
    // [ForeignKey(nameof(Location)), Column(TypeName = "nvarchar(36)")]
    // public string? LocationId { get; set; }
    // public EventLocationEntity? Location { get; set; }
    

    public ICollection<EventPackageEntity> Packages { get; set; } = [];
}