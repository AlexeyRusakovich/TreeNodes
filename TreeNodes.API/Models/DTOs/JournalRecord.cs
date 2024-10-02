using System.Text.Json.Serialization;

namespace TreeNodes.API.Models.DTOs
{
    public class JournalRecord
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public TreeExceptionData Data { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
    }
}
