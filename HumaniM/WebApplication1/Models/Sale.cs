using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? EditedOn { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? EndedOn { get; set; }
        public int RefLocationId { get; set; }
    }
}
