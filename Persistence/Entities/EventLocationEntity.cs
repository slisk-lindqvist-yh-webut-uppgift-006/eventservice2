// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
//
// namespace Persistence.Entities;
//
// public class EventLocationEntity
// {
//     [Key, Column(TypeName = "nvarchar(36)")]
//     public string Id { get; set; } = Guid.NewGuid().ToString();
//
//     [Column(TypeName = "nvarchar(100)")]
//     public string? StreetName { get; set; }
//
//     [Column(TypeName = "nvarchar(12)")]
//     public string? PostalCode { get; set; }
//
//     [Column(TypeName = "nvarchar(100)")]
//     public string? City { get; set; }
//
//     [Column(TypeName = "nvarchar(75)")]
//     public string? Country { get; set; }
//
//     #region ChatGPT Advice
//
//         [Column(TypeName = "nvarchar(100)")]
//         public string? VenueName { get; set; }
//
//         [Column(TypeName = "nvarchar(25)")]
//         public string? StateOrProvince { get; set; }
//
//         [NotMapped]
//         public string Location =>
//             string.Join(", ", new[] { VenueName, City, StateOrProvince }.Where(x => !string.IsNullOrWhiteSpace(x)));
//
//     #endregion
// }