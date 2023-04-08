using System.ComponentModel.DataAnnotations;

namespace QuestionBulkUpload.Models
{
    public class QuestionData
    {
        [Key]
        public int Id { get; set; }
        public string? Direction { get; set; }
        public string? Summary { get; set; }
        public string? QuestionS { get; set; }
        public string? Explaination { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }
    }
}
