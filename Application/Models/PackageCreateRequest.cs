using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class PackageCreateRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [Display(Name = "Package Title", Prompt = "Enter package title")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Seating arrangement is required.")]
    [Display(Name = "Seating Arrangement", Prompt = "Enter seating arrangement")]
    public string SeatingArrangement { get; set; } = null!;

    [Display(Name = "Placement", Prompt = "Enter placement (optional)")]
    public string? Placement { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Display(Name = "Ticket Price", Prompt = "Enter ticket price")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "Currency", Prompt = "Enter currency code (e.g., USD)")]
    public string? Currency { get; set; }
}