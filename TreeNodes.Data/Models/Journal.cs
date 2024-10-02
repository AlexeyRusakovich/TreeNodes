using System.ComponentModel.DataAnnotations;

namespace TreeNodes.Data.Models
{
    public class Journal
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
