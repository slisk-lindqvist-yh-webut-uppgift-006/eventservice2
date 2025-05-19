using System.ComponentModel.DataAnnotations;
using Persistence.Entities;

namespace Application.Models;

public class CreateEventRequest
{
    [Display(Name = "Image URL", Prompt = "Enter image URL")]
    [MaxLength(255, ErrorMessage = "Image URL cannot exceed 255 characters")]
    public string? Image { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
    [Display(Name = "Title", Prompt = "Enter event title")]
    public string Title { get; set; } = null!;

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    [Display(Name = "Description", Prompt = "Enter event description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Event date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Event Date", Prompt = "Select the event date")]
    public DateTime EventDate { get; set; }
    
    // [Required(ErrorMessage = "Date is required")]
    // [DataType(DataType.Date)]
    // [Display(Name = "Event Date", Prompt = "Select the date of the event")]
    // public DateTime Date { get; set; }
    //
    // [Required(ErrorMessage = "Time is required")]
    // [DataType(DataType.Time)]
    // [Display(Name = "Event Time", Prompt = "Select the time of the event")]
    // public TimeSpan Time { get; set; }

    // [Display(Name = "Existing Location", Prompt = "Select existing location ID (optional)")]
    // public string? LocationId { get; set; }
    //
    // [Display(Name = "New Location Details", Prompt = "Enter new location details")]
    // public EventLocationEntity? Location { get; set; }

    [MaxLength(300, ErrorMessage = "Location cannot exceed 300 characters")]
    [Display(Name = "Location", Prompt = "Enter the location (e.g., Venue, City, Country)")]
    public string? Location { get; set; }
    
    // [Display(Name = "Event Packages", Prompt = "Select associated package IDs")]
    // public List<string>? PackageIds { get; set; }
}