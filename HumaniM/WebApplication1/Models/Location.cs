using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Location
    {
        [Key]
        public int Id { get; }
        public string? Name { get;}
        public string? AddressLine1 { get; }
        public string? PostalCode { get; }
        public string? Country { get; }
    }
}
