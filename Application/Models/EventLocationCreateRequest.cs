using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class EventLocationCreateRequest
{
    [MaxLength(100)]
    [Display(Name = "Venue Name", Prompt = "Enter venue name")]
    public string? VenueName { get; set; } = null!;

    [MaxLength(100)]
    [Display(Name = "City", Prompt = "Enter city name")]
    public string? City { get; set; } = null!;

    [MaxLength(25)]
    [Display(Name = "State or Province", Prompt = "Enter state or province")]
    public string? StateOrProvince { get; set; } = null!;

    [MaxLength(100)]
    [Display(Name = "Street Name", Prompt = "Enter street name")]
    public string? StreetName { get; set; }

    [MaxLength(12)]
    [Display(Name = "Postal Code", Prompt = "Enter postal code")]
    public string? PostalCode { get; set; }

    [MaxLength(75)]
    [Display(Name = "Country", Prompt = "Enter country name")]
    public string? Country { get; set; }
}